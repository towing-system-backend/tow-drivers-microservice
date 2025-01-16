using Application.Core;
using Moq;
using TowDriver.Application;
using TowDriver.Domain;
using Xunit;

namespace TowDriver.Test
{
    public class UpdateTowDriverLocationCommandHandlerTests
    {
        private readonly Mock<IMessageBrokerService> _messageBrokerServiceMock;
        private readonly Mock<IEventStore> _eventStoreMock;
        private readonly Mock<ITowDriverRepository> _towDriverRepositoryMock;
        private readonly UpdateTowDriverLocationCommandHandler _updateTowDriverLocationCommandHandler;

        public UpdateTowDriverLocationCommandHandlerTests()
        {
            _messageBrokerServiceMock = new Mock<IMessageBrokerService>();
            _eventStoreMock = new Mock<IEventStore>();
            _towDriverRepositoryMock = new Mock<ITowDriverRepository>();
            _updateTowDriverLocationCommandHandler = new UpdateTowDriverLocationCommandHandler(
                _messageBrokerServiceMock.Object,
                _eventStoreMock.Object,
                _towDriverRepositoryMock.Object
            );
        }

        [Fact]
        public async Task Should_Not_Update_Location_When_TowDriver_Not_Found()
        {
            // Arrange
            var command = new UpdateTowDriverLocationCommand("non_existent_driver_id", "New Location");

            _towDriverRepositoryMock.Setup(repo => repo.FindById(command.TowDriverId))
                .ReturnsAsync(Optional<Domain.TowDriver>.Empty());

            // Act
            var result = await _updateTowDriverLocationCommandHandler.Execute(command);

            // Assert
            Assert.True(result.IsError);
            var exception = Assert.IsType<TowDriverNotFound>(result.Unwrap());
            Assert.Equal("Tow driver not found.", exception.Message);

            _towDriverRepositoryMock.Verify(repo => repo.Save(It.IsAny<Domain.TowDriver>()), Times.Never);
            _eventStoreMock.Verify(store => store.AppendEvents(It.IsAny<List<DomainEvent>>()), Times.Never);
            _messageBrokerServiceMock.Verify(service => service.Publish(It.IsAny<List<DomainEvent>>()), Times.Never);
        }

        [Fact]
        public async Task Should_Update_Location_Successfully()
        {
            // Arrange
            var command = new UpdateTowDriverLocationCommand("existing_driver_id", "New Location");

            var towDriver = Domain.TowDriver.Create(
               new TowDriverId(command.TowDriverId),
               new SupplierCompanyId("6ec8ef54-9bdc-4abb-888a-829ca6152524"),
               new TowDriverName("John Doe"),
               new TowDriverEmail("john.doe@example.com"),
               new TowDriverDrivingLicense(
                   "John Doe",
                   DateOnly.FromDateTime(DateTime.Now.AddYears(-5)),
                   DateOnly.FromDateTime(DateTime.Now.AddYears(5))
               ),
               new TowDriverMedicalCertificate(
                   "John Doe",
                   30,
                   DateOnly.FromDateTime(DateTime.Now.AddYears(-1)),
                   DateOnly.FromDateTime(DateTime.Now.AddYears(1))
               ),
               new TowDriverIdentificationNumber(24451458),
               new TowDriverLocation("Unknown"),
               new TowDriverStatus("Inactive")
           );

            _towDriverRepositoryMock.Setup(repo => repo.FindById(command.TowDriverId))
                .ReturnsAsync(Optional<Domain.TowDriver>.Of(towDriver));

            // Act
            var result = await _updateTowDriverLocationCommandHandler.Execute(command);

            // Assert
            Assert.False(result.IsError);
            Assert.Equal(command.TowDriverId, result.Unwrap().TowDriverId);

            _towDriverRepositoryMock.Verify(repo => repo.Save(It.Is<Domain.TowDriver>(td =>
                td.GetTowDriverId().GetValue() == command.TowDriverId &&
                td.GetTowDriverLocation().GetValue() == "New Location"
            )), Times.Once);

            _eventStoreMock.Verify(store => store.AppendEvents(It.IsAny<List<DomainEvent>>()), Times.Once);
            _messageBrokerServiceMock.Verify(service => service.Publish(It.IsAny<List<DomainEvent>>()), Times.Once);
        }
    }
}

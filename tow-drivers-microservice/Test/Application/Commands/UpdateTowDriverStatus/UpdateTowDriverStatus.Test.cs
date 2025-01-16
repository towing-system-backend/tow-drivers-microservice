using Application.Core;
using Moq;
using TowDriver.Application;
using TowDriver.Domain;
using Xunit;

namespace TowDriver.Test
{
    public class UpdateTowDriverStatusCommandHandlerTests
    {
        private readonly Mock<IMessageBrokerService> _messageBrokerServiceMock;
        private readonly Mock<IEventStore> _eventStoreMock;
        private readonly Mock<ITowDriverRepository> _towDriverRepositoryMock;
        private readonly UpdateTowDriverStatusCommandHandler _updateTowDriverStatusCommandHandler;

        public UpdateTowDriverStatusCommandHandlerTests()
        {
            _messageBrokerServiceMock = new Mock<IMessageBrokerService>();
            _eventStoreMock = new Mock<IEventStore>();
            _towDriverRepositoryMock = new Mock<ITowDriverRepository>();
            _updateTowDriverStatusCommandHandler = new UpdateTowDriverStatusCommandHandler(
                _messageBrokerServiceMock.Object,
                _eventStoreMock.Object,
                _towDriverRepositoryMock.Object
            );
        }

        [Fact]
        public async Task Should_Not_Update_Status_When_TowDriver_Not_Found()
        {
            // Arrange
            var command = new UpdateTowDriverStatusCommand("3bbba3f3-bf43-4050-a5b4-5cad69a7462d", "Active");

            _towDriverRepositoryMock.Setup(repo => repo.FindById(command.TowDriverId))
                .ReturnsAsync(Optional<Domain.TowDriver>.Empty());

            // Act
            var result = await _updateTowDriverStatusCommandHandler.Execute(command);

            // Assert
            Assert.True(result.IsError);
            var exception = Assert.Throws<TowDriverNotFound>(() => result.Unwrap());
            Assert.Equal("Tow driver not found.", exception.Message);

            _towDriverRepositoryMock.Verify(repo => repo.Save(It.IsAny<Domain.TowDriver>()), Times.Never);
            _eventStoreMock.Verify(store => store.AppendEvents(It.IsAny<List<DomainEvent>>()), Times.Never);
            _messageBrokerServiceMock.Verify(service => service.Publish(It.IsAny<List<DomainEvent>>()), Times.Never);
        }

        [Fact]
        public async Task Should_Update_Status_Successfully()
        {
            // Arrange
            var command = new UpdateTowDriverStatusCommand("3bbba3f3-bf43-4050-a5b4-5cad69a7462d", "Active");

            var towDriver = Domain.TowDriver.Create(
                new TowDriverId(command.TowDriverId),
                new SupplierCompanyId("3bbba3f3-bf43-4050-a5b4-5cad69a7462d"),
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
            var result = await _updateTowDriverStatusCommandHandler.Execute(command);

            // Assert
            Assert.False(result.IsError);
            Assert.Equal(command.TowDriverId, result.Unwrap().TowDriverId);

            _towDriverRepositoryMock.Verify(repo => repo.Save(It.Is<Domain.TowDriver>(td =>
                    td.GetTowDriverId().GetValue() == command.TowDriverId &&
                    td.GetTowDriverStatus().GetValue() == command.Status
                )
            ), Times.Once);

            _eventStoreMock.Verify(store => store.AppendEvents(It.Is<List<DomainEvent>>(events =>
                    events.Count == 1 &&
                    events.Any(e => e is TowDriverStatusUpdatedEvent)
                )
            ), Times.Once);

            _messageBrokerServiceMock.Verify(service => service.Publish(It.Is<List<DomainEvent>>(events =>
                    events.Count == 1 &&
                    events[0] is TowDriverStatusUpdatedEvent
                )
            ), Times.Once);
        }
    }
}

using Application.Core;
using Moq;
using TowDriver.Application;
using TowDriver.Domain;
using Xunit;

namespace TowDriver.Tests
{
    public class CreateTowDriverCommandHandlerTests
    {
        private readonly Mock<IdService<string>> _idServiceMock;
        private readonly Mock<IMessageBrokerService> _messageBrokerServiceMock;
        private readonly Mock<IEventStore> _eventStoreMock;
        private readonly Mock<ITowDriverRepository> _towDriverRepositoryMock;
        private readonly CreateTowDriverCommandHandler _createTowDriverCommandHandler;

        public CreateTowDriverCommandHandlerTests()
        {
            _idServiceMock = new Mock<IdService<string>>();
            _messageBrokerServiceMock = new Mock<IMessageBrokerService>();
            _eventStoreMock = new Mock<IEventStore>();
            _towDriverRepositoryMock = new Mock<ITowDriverRepository>();
            _createTowDriverCommandHandler = new CreateTowDriverCommandHandler(
                _idServiceMock.Object,
                _messageBrokerServiceMock.Object,
                _eventStoreMock.Object,
                _towDriverRepositoryMock.Object
            );
        }

        /*

        [Fact]
        public async Task Should_Not_Create_TowDriver_When_Email_Already_Exists()
        {
            // Arrange
            var command = new CreateTowDriverCommand(
                "John Doe",
                "john.doe@example.com",
                "John Doe",
                DateOnly.FromDateTime(DateTime.Now.AddYears(-5)),
                DateOnly.FromDateTime(DateTime.Now.AddYears(5)),
                "John Doe",
                30,
                DateOnly.FromDateTime(DateTime.Now.AddYears(-1)),
                DateOnly.FromDateTime(DateTime.Now.AddYears(1)),
                123456789
            );

            var existingTowDriver = Domain.TowDriver.Create(
                new TowDriverId("existing_tow_driver_id"),
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
                new TowDriverIdentificationNumber(123456789),
                new TowDriverLocation("UnKnow"),
                new TowDriverStatus("Inactive")
            );

            _towDriverRepositoryMock.Setup(repo => repo.FindByEmail(command.TowDriverEmail))
                .ReturnsAsync(Optional<TowDriver>.Of(existingTowDriver));

            // Act
            var result = await _createTowDriverCommandHandler.Execute(command);

            // Assert
            Assert.True(result.IsError);
            var exception = Assert.IsType<TowDriverAlreadyExists>(result.Unwrap());
            Assert.Equal("Tow driver with the provided email already exists.", exception.Message);

            _towDriverRepositoryMock.Verify(repo => repo.Save(It.IsAny<TowDriver>()), Times.Never);
            _eventStoreMock.Verify(store => store.AppendEvents(It.IsAny<List<DomainEvent>>()), Times.Never);
            _messageBrokerServiceMock.Verify(service => service.Publish(It.IsAny<List<DomainEvent>>()), Times.Never);
        }

        
        [Fact]
        public async Task Should_Create_TowDriver_Successfully()
        {
            // Arrange
            var command = new CreateTowDriverCommand(
                "John Doe",
                "john.doe@example.com",
                "John Doe",
                DateOnly.FromDateTime(DateTime.Now.AddYears(-5)),
                DateOnly.FromDateTime(DateTime.Now.AddYears(5)),
                "John Doe",
                30,
                DateOnly.FromDateTime(DateTime.Now.AddYears(-1)),
                DateOnly.FromDateTime(DateTime.Now.AddYears(1)),
                123456789
            );

            _towDriverRepositoryMock.Setup(repo => repo.FindByEmail(command.TowDriverEmail))
                .ReturnsAsync(Optional<Domain.TowDriver>.Empty());

            _idServiceMock.Setup(service => service.GenerateId())
                .Returns("new_tow_driver_id");

            // Act
            var result = await _createTowDriverCommandHandler.Execute(command);

            // Assert
            Assert.False(result.IsError);
            Assert.Equal("new_tow_driver_id", result.Unwrap().TowDriverId);

            _towDriverRepositoryMock.Verify(repo => repo.Save(It.Is<Domain.TowDriver>(td =>
                td.GetId().GetValue() == "new_tow_driver_id" &&
                td.GetName().GetValue() == command.TowDriverName &&
                td.GetEmail().GetValue() == command.TowDriverEmail &&
                td.GetDrivingLicense().OwnerName == command.LicenseOwnerName &&
                td.GetDrivingLicense().IssueDate == command.LicenseIssueDate &&
                td.GetDrivingLicense().ExpirationDate == command.LicenseExpirationDate &&
                td.GetMedicalCertificate().OwnerName == command.MedicalCertificateOwnerName &&
                td.GetMedicalCertificate().OwnerAge == command.MedicalCertificateOwnerAge &&
                td.GetMedicalCertificate().IssueDate == command.MedicalCertificateIssueDate &&
                td.GetMedicalCertificate().ExpirationDate == command.MedicalCertificateExpirationDate &&
                td.GetIdentificationNumber().GetValue() == command.TowDriverIdentificationNumber &&
                td.GetLocation().GetValue() == "UnKnow" &&
                td.GetStatus().GetValue() == "Inactive"
            )), Times.Once);

            _eventStoreMock.Verify(store => store.AppendEvents(It.Is<List<DomainEvent>>(events =>
                events.Count == 1 &&
                events[0] is TowDriverCreatedEvent
            )), Times.Once);

            _messageBrokerServiceMock.Verify(service => service.Publish(It.Is<List<DomainEvent>>(events =>
                events.Count == 1 &&
                events[0] is TowDriverCreatedEvent
            )), Times.Once);
        }
        */

    }
}

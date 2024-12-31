using Application.Core;
using TowDrivers.Application;

namespace TowDrivers.Domain
{
    public class CreateTowDriverCommandHandler
    (
        IEventStore eventStore,
        IdService<string> idService,
        ITowDriverRepository towDriverRepository,
        IMessageBrokerService messageBrokerService
    ) : IService<CreateTowDriverCommand, CreateTowDriverResponse>
    {
        
        private readonly IEventStore _eventStore =  eventStore;
        private readonly IdService<string> _idService = idService;
        private readonly ITowDriverRepository _towDriverRepository = towDriverRepository;
        private readonly IMessageBrokerService _messageBrokerService = messageBrokerService;
        public async Task<Result<CreateTowDriverResponse>> Execute(CreateTowDriverCommand command)
        {
            var id = _idService.GenerateId();
            var towDriver = TowDriver.Create
            (
                new TowDriverId(id),
                new TowDriverName(command.towDriverName),
                new TowDriverEmail(command.towDriverEmail),
                new TowDriverDrivingLicense(
                    command.licenseOwnerName,
                    command.licenseIssueDate,
                    command.licenseExpirationDate
                ),
                new TowDriverMedicalCertificate(
                    command.medicalCertificateOwnerName,
                    command.medicaCertificateAge,
                    command.medicalCertificateIssueDate,
                    command.medicalCertificateExpirationDate
                ),
                new TowDriverIdentificationNumber(command.towDriverIdentificationNumber),
                new TowDriverLocation(""),
                new TowDriverStatus("Inactive")
            );

            var events = towDriver.PullEvents();
            await _towDriverRepository.Save(towDriver);
            await _eventStore.AppendEvents(events);
            await _messageBrokerService.Publish(events);

            return Result<CreateTowDriverResponse>.MakeSuccess(new CreateTowDriverResponse(id));

        }

    }
}

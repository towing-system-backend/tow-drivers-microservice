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
                new TowDriverName(command.TowDriverName),
                new TowDriverEmail(command.TowDriverEmail),
                new TowDriverDrivingLicense(
                    command.LicenseOwnerName,
                    command.LicenseIssueDate,
                    command.LicenseExpirationDate
                ),
                new TowDriverMedicalCertificate(
                    command.MedicalCertificateOwnerName,
                    command.MedicalCertificateAge,
                    command.MedicalCertificateIssueDate,
                    command.MedicalCertificateExpirationDate
                ),
                new TowDriverIdentificationNumber(command.TowDriverIdentificationNumber),
                new TowDriverLocation("UnKnow"),
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

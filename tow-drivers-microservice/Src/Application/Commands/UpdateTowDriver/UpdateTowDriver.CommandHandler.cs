using Application.Core;
using TowDrivers.Application;

namespace TowDrivers.Domain
{
    public class UpdateTowDriverCommandHandler
    (
        IEventStore eventStore,
        IdService<string> idService,
        ITowDriverRepository towDriverRepository,
        IMessageBrokerService messageBrokerService
    )
    : IService<UpdateTowDriverCommand, UpdateTowDriverResponse>
    {
        private readonly IEventStore _eventStore = eventStore;
        private readonly IdService<string> _idService = idService;
        private readonly ITowDriverRepository _towDriverRepository = towDriverRepository;
        private readonly IMessageBrokerService _messageBrokerService = messageBrokerService;

        public async Task<Result<UpdateTowDriverResponse>> Execute(UpdateTowDriverCommand command)
        {
            var towDriverRegistred = await _towDriverRepository.FindById(command.TowDriverId);
            if (towDriverRegistred == null) Result<UpdateTowDriverResponse>.MakeError(new TowDriverNotFoundError());
            var towDriver = towDriverRegistred.Unwrap();

            if (command.TowDriverName != null) towDriver.UpdateDriverName(new TowDriverName(command.TowDriverName));
            if (command.TowDriverEmail != null) towDriver.UpdateDriverEmail(new TowDriverEmail(command.TowDriverEmail));
            if (command.LicenseOwnerName != null && command.LicenseIssueDate != null && command.LicenseExpirationDate != null)
                towDriver.UpdateDriverDrivingLicense(
                    new TowDriverDrivingLicense(
                        command.LicenseOwnerName,
                        command.LicenseIssueDate,
                        command.LicenseExpirationDate
                    )
                );
            if (command.MedicalCertificateOwnerName != null && 
                command.MedicalCertificateAge != null && 
                command.MedicalCertificateIssueDate != null && 
                command.MedicalCertificateExpirationDate != null)
                towDriver.UpdateDriverMedicalCertificate(
                    new TowDriverMedicalCertificate(
                        command.MedicalCertificateOwnerName,
                        command.MedicalCertificateAge,
                        command.MedicalCertificateIssueDate,
                        command.MedicalCertificateExpirationDate
                    )
                );
            if (command.TowDriverIdentificationNumber != null)
                towDriver.UpdateDriverIdentificationNumber(new TowDriverIdentificationNumber(command.TowDriverIdentificationNumber));

            var events = towDriver.PullEvents();
            await _towDriverRepository.Save(towDriver);
            await _eventStore.AppendEvents(events);
            await _messageBrokerService.Publish(events);

            return Result<UpdateTowDriverResponse>.MakeSuccess(new UpdateTowDriverResponse(towDriver.GetTowDriverId().GetValue()));
        }
    }
}

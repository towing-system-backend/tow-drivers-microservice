using Application.Core;
using TowDriver.Domain;

namespace TowDriver.Application
{
    public class UpdateTowDriverCommandHandler
    (
        IMessageBrokerService messageBrokerService,
        IEventStore eventStore,
        ITowDriverRepository towDriverRepository
    )
    : IService<UpdateTowDriverCommand, UpdateTowDriverResponse>
    {
        private readonly IMessageBrokerService _messageBrokerService = messageBrokerService;
        private readonly IEventStore _eventStore = eventStore;
        private readonly ITowDriverRepository _towDriverRepository = towDriverRepository;

        public async Task<Result<UpdateTowDriverResponse>> Execute(UpdateTowDriverCommand command)
        {
            var towDriverRegistred = await _towDriverRepository.FindById(command.TowDriverId);
            if (towDriverRegistred == null) return Result<UpdateTowDriverResponse>.MakeError(new TowDriverNotFound());
            var towDriver = towDriverRegistred.Unwrap();

            if (command.SupplierCompanyId != null) towDriver.UpdateSupplierCompanyId(new SupplierCompanyId(command.SupplierCompanyId));
            if (command.TowDriverName != null) towDriver.UpdateDriverName(new TowDriverName(command.TowDriverName));
            if (command.TowDriverEmail != null) towDriver.UpdateDriverEmail(new TowDriverEmail(command.TowDriverEmail));
            if (command.LicenseOwnerName != null && command.LicenseIssueDate != null && command.LicenseExpirationDate != null)
                towDriver.UpdateDriverDrivingLicense(
                    new TowDriverDrivingLicense(
                        command.LicenseOwnerName,
                        (DateOnly) command.LicenseIssueDate,
                        (DateOnly) command.LicenseExpirationDate
                    )
                );
            if (command.MedicalCertificateOwnerName != null && 
                command.MedicalCertificateAge != null && 
                command.MedicalCertificateIssueDate != null && 
                command.MedicalCertificateExpirationDate != null)
                    towDriver.UpdateDriverMedicalCertificate(
                        new TowDriverMedicalCertificate(
                            command.MedicalCertificateOwnerName,
                            (int) command.MedicalCertificateAge,
                            (DateOnly) command.MedicalCertificateIssueDate,
                            (DateOnly) command.MedicalCertificateExpirationDate
                        )
                );
            if (command.TowDriverIdentificationNumber != null)
                towDriver.UpdateDriverIdentificationNumber(new TowDriverIdentificationNumber((int) command.TowDriverIdentificationNumber));

            var events = towDriver.PullEvents();
            await _towDriverRepository.Save(towDriver);
            await _eventStore.AppendEvents(events);
            await _messageBrokerService.Publish(events);
            
            return Result<UpdateTowDriverResponse>.MakeSuccess(new UpdateTowDriverResponse(command.TowDriverId));
        }
    }
}

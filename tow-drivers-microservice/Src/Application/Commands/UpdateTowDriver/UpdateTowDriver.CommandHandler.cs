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
            var towDriverRegistred = await _towDriverRepository.FindById(command.towDriverId);
            if (towDriverRegistred == null) Result<UpdateTowDriverResponse>.MakeError(new TowDriverNotFoundError());
            var towDriver = towDriverRegistred.Unwrap();

            if (command.towDriverName != null) towDriver.UpdateDriverName(new TowDriverName(command.towDriverName));
            if (command.towDriverEmail != null) towDriver.UpdateDriverEmail(new TowDriverEmail(command.towDriverEmail));
            if (command.licenseOwnerName != null && command.licenseIssueDate != null && command.licenseExpirationDate != null)
                towDriver.UpdateDriverDrivingLicense(
                    new TowDriverDrivingLicense(
                        command.licenseOwnerName,
                        command.licenseIssueDate,
                        command.licenseExpirationDate
                    )
                );
            if (command.medicalCertificateOwnerName != null && command.medicalCertificateAge != null && command.medicalCertificateIssueDate != null && command.medicalCertificateExpirationDate != null)
                towDriver.UpdateDriverMedicalCertificate(
                    new TowDriverMedicalCertificate(
                        command.medicalCertificateOwnerName,
                        command.medicalCertificateAge,
                        command.medicalCertificateIssueDate,
                        command.medicalCertificateExpirationDate
                    )
                );
            if (command.towDriverIdentificationNumber != null)
                towDriver.UpdateDriverIdentificationNumber(new TowDriverIdentificationNumber(command.towDriverIdentificationNumber));

            var events = towDriver.PullEvents();
            await _towDriverRepository.Save(towDriver);
            await _eventStore.AppendEvents(events);
            await _messageBrokerService.Publish(events);

            return Result<UpdateTowDriverResponse>.MakeSuccess(new UpdateTowDriverResponse(towDriver.GetTowDriverId().GetValue()));
        }
    }
}

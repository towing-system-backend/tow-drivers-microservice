using Application.Core;
using TowDriver.Domain;

namespace TowDriver.Application
{
    public class CreateTowDriverCommandHandler
    (
        IdService<string> idService,
        IMessageBrokerService messageBrokerService,
        IEventStore eventStore,
        ITowDriverRepository towDriverRepository,
        ISupplierCompanyRespository supplierCompanyRespository
    ) : IService<CreateTowDriverCommand, CreateTowDriverResponse>
    {
        private readonly IdService<string> _idService = idService;
        private readonly IMessageBrokerService _messageBrokerService = messageBrokerService;
        private readonly IEventStore _eventStore =  eventStore;
        private readonly ITowDriverRepository _towDriverRepository = towDriverRepository;
        private readonly ISupplierCompanyRespository _supplierCompanyRespository = supplierCompanyRespository;
        public async Task<Result<CreateTowDriverResponse>> Execute(CreateTowDriverCommand command)
        {
            var towDriverRegistered = await _towDriverRepository.FindByEmail(command.TowDriverEmail);
            if (towDriverRegistered.HasValue()) return Result<CreateTowDriverResponse>.MakeError(new TowDriverAlreadyExists());

            var id = _idService.GenerateId();
            var towDriver = Domain.TowDriver.Create
            (
                new TowDriverId(command.TowDriverId),
                new SupplierCompanyId(command.SupplierCompanyId),
                new TowDriverName(command.TowDriverName),
                new TowDriverEmail(command.TowDriverEmail),
                new TowDriverDrivingLicense(
                    command.LicenseOwnerName,
                    command.LicenseIssueDate,
                    command.LicenseExpirationDate
                ),
                new TowDriverMedicalCertificate(
                    command.MedicalCertificateOwnerName,
                    command.MedicalCertificateOwnerAge,
                    command.MedicalCertificateIssueDate,
                    command.MedicalCertificateExpirationDate
                ),
                new TowDriverIdentificationNumber(command.TowDriverIdentificationNumber),
                new TowDriverLocation("UnKnow"),
                new TowDriverStatus("Inactive"),
                new TowDriverTowAssigned("Not assigned")
            );

            var events = towDriver.PullEvents();
            await _towDriverRepository.Save(towDriver);
            await _supplierCompanyRespository.SaveTowDriver(towDriver);
            await _eventStore.AppendEvents(events);
            await _messageBrokerService.Publish(events);

            return Result<CreateTowDriverResponse>.MakeSuccess(new CreateTowDriverResponse(id));
        }
    }
}
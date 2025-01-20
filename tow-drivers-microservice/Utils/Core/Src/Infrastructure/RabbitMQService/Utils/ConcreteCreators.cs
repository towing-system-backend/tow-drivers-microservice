using TowDriver.Infrastructure;

namespace RabbitMQ.Contracts
{
    public class CreateTowDriverDtoCreator : DtoCreator<CreateTowDriver, CreateTowDriverDto>
    {
        public override CreateTowDriverDto CreateDto(CreateTowDriver message)
        {
            return new CreateTowDriverDto(
                message.Id,
                message.SupplierCompanyId,
                message.Name,
                message.Email,
                message.LicenseOwnerName,
                message.LicenseIssueDate,
                message.LicenseExpirationDate,
                message.MedicalCertificateOwnerName,
                message.MedicalCertificateAge,
                message.MedicalCertificateIssueDate,
                message.MedicalCertificateExpirationDate,
                message.IdentificationNumber
            );
        }
    }
}
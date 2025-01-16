namespace RabbitMQ.Contracts
{
    public interface IRabbitMQMessage { };

    public record EventType(
        string PublisherId,
        string Type,
        object Context,
        DateTime OcurredDate
    );

    public record CreateTowDriver(
        string Id,
        string SupplierCompanyId,
        string Name,
        string Email,
        string LicenseOwnerName,
        DateOnly LicenseIssueDate,
        DateOnly LicenseExpirationDate,
        string MedicalCertificateOwnerName,
        int MedicalCertificateAge,
        DateOnly MedicalCertificateIssueDate,
        DateOnly MedicalCertificateExpirationDate,
        int IdentificationNumber
    ) : IRabbitMQMessage;
}
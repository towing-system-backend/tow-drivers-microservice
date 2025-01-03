namespace tow_drivers_microservice.Src.Infrastructure.Controllers.Responses
{
    public record FindActiveTowDriversResponse
    (
        string towDriverId,
        string towDriverName,
        string towDriverEmail,
        string licenseOwnerName,
        DateOnly licenseIssueDate,
        DateOnly licenseExpirationDate,
        string medicalCertificateOwnerName,
        int medicalCertificateAge,
        DateOnly medicalCertificateIssueDate,
        DateOnly medicalCertificateExpirationDate,
        int towDriverIdentificationNumber,
        string location,
        string status
    );
}

namespace TowDrivers.Application
{
    public record UpdateTowDriverCommand
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
        int towDriverIdentificationNumber
    );
}

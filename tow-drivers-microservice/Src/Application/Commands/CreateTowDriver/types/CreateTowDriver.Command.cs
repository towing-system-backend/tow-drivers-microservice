namespace TowDrivers.Domain
{
    public record CreateTowDriverCommand
    (
        string towDriverName,
        string towDriverEmail,
        string licenseOwnerName,
        DateOnly licenseIssueDate,
        DateOnly licenseExpirationDate,
        string medicalCertificateOwnerName,
        int medicaCertificateAge,
        DateOnly medicalCertificateIssueDate,
        DateOnly medicalCertificateExpirationDate,
        int towDriverIdentificationNumber
    );
}

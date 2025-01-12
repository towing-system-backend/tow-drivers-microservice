namespace TowDriver.Application
{
    public record CreateTowDriverCommand
    (
        string TowDriverName,
        string TowDriverEmail,
        string LicenseOwnerName,
        DateOnly LicenseIssueDate,
        DateOnly LicenseExpirationDate,
        string MedicalCertificateOwnerName,
        int MedicalCertificateOwnerAge,
        DateOnly MedicalCertificateIssueDate,
        DateOnly MedicalCertificateExpirationDate,
        int TowDriverIdentificationNumber
    );
}

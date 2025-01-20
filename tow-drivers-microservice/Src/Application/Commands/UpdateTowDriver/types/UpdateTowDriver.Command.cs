namespace TowDriver.Application
{
    public record UpdateTowDriverCommand
    (
        string TowDriverId,
        string? SupplierCompanyId,
        string? TowDriverName,
        string? TowDriverEmail,
        string? LicenseOwnerName,
        DateOnly? LicenseIssueDate,
        DateOnly? LicenseExpirationDate,
        string? MedicalCertificateOwnerName,
        int? MedicalCertificateAge,
        DateOnly? MedicalCertificateIssueDate,
        DateOnly? MedicalCertificateExpirationDate,
        int? TowDriverIdentificationNumber,
        string TowDriverTowAssigned
    );
}

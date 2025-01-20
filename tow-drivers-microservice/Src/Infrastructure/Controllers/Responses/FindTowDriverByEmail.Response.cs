namespace TowDriver.Infrastructure
{
    public record FindTowDriverByEmailResponse 
    (
        string TowDriverId,
        string TowDriverName,
        string TowDriverEmail,
        string LicenseOwnerName,
        DateOnly LicenseIssueDate,
        DateOnly LicenseExpirationDate,
        string MedicalCertificateOwnerName,
        int MedicalCertificateAge,
        DateOnly MedicalCertificateIssueDate,
        DateOnly MedicalCertificateExpirationDate,
        int TowDriverIdentificationNumber,
        string? Location,
        string? Status,
        string TowAssigned
    );
}

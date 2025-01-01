using Application.Core;
using TowDrivers.Infrastructure;

namespace TowDrivers.Domain 
{
    public class TowDriverCreatedEvent(string publisherId, string type, TowDriverCreated context) 
        : DomainEvent(publisherId, type, context) { }

    public class TowDriverCreated
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
        string? towDriverLocation,
        string? towDriverStatus
    )
    {
        public readonly string TowDriverId = towDriverId;
        public readonly string TowDriverName = towDriverName;
        public readonly string TowDriverEmail = towDriverEmail;
        public readonly string LicenseOwnerName = licenseOwnerName;
        public readonly DateOnly LicenseIssueDate = licenseIssueDate;
        public readonly DateOnly LicenseExpirationDate = licenseExpirationDate;
        public readonly string MedicalCertificateOwnerName = medicalCertificateOwnerName;
        public readonly int MedicalCertificateAge = medicalCertificateAge;
        public readonly DateOnly MedicalCertificateIssueDate = medicalCertificateIssueDate;
        public readonly DateOnly MedicalCertificateExpirationDate = medicalCertificateExpirationDate;
        public readonly int TowDriverIdentificationNumber = towDriverIdentificationNumber;
        public readonly string? TowDriverLocation = towDriverLocation;
        public readonly string TowDriverStatus = "Inactive";

        public static TowDriverCreatedEvent CreateEvent(
            TowDriverId towDriverId,
            TowDriverName towDriverName,
            TowDriverEmail towDriverEmail,
            TowDriverDrivingLicense towDriverDrivingLicense,
            TowDriverMedicalCertificate towDriverMedicalCertificate,
            TowDriverIdentificationNumber towDriverIdentificationNumber,
            TowDriverLocation towDriverLocation,
            TowDriverStatus towDriverStatus
        )
        {
            return new TowDriverCreatedEvent
            (
                towDriverId.GetValue(),
                typeof(TowDriverCreated).Name,
                new TowDriverCreated
                (
                    towDriverId.GetValue(),
                    towDriverName.GetValue(),
                    towDriverEmail.GetValue(),
                    towDriverDrivingLicense.GetOwnerName(),
                    towDriverDrivingLicense.GetIssueDate(),
                    towDriverDrivingLicense.GetExpirationDate(),
                    towDriverMedicalCertificate.GetOwnerName(),
                    towDriverMedicalCertificate.GetOwnerAge(),
                    towDriverMedicalCertificate.GetIssueDate(),
                    towDriverMedicalCertificate.GetExpirationDate(),
                    towDriverIdentificationNumber.GetValue(),
                    towDriverLocation?.GetValue(),
                    towDriverStatus.GetValue()
                )
            );
        }
    }
    
    
}

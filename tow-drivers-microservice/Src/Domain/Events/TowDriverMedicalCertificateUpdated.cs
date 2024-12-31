using Application.Core;

namespace TowDrivers.Domain 
{
    public class TowDriverMedicalCertificateUpdatedEvent(string publisherId, string type, TowDriverMedicalCertificateUpdated context) : DomainEvent(publisherId, type, context) { }

    public class TowDriverMedicalCertificateUpdated(string ownerName, int ownerAge, DateOnly issueDate, DateOnly expirationDate)
    {
        public readonly string OwnerName = ownerName;
        public readonly int OwnerAge = ownerAge;
        public readonly DateOnly IssueDate = issueDate;
        public readonly DateOnly ExpirationDate = expirationDate;

        public static TowDriverMedicalCertificateUpdatedEvent CreateEvent(TowDriverId publisherId, TowDriverMedicalCertificate medicalCertificate)
        {
            return new TowDriverMedicalCertificateUpdatedEvent(
                publisherId.GetValue(),
                typeof(TowDriverMedicalCertificateUpdated).Name,
                new TowDriverMedicalCertificateUpdated(
                    medicalCertificate.GetOwnerName(),
                    medicalCertificate.GetOwnerAge(),
                    medicalCertificate.GetIssueDate(),
                    medicalCertificate.GetExpirationDate()
                )
            );
        }
    }
}

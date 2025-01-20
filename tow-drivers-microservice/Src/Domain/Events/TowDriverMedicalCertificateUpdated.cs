using Application.Core;

namespace TowDriver.Domain
{
    public class TowDriverMedicalCertificateUpdatedEvent(string publisherId, string type, TowDriverMedicalCertificateUpdated context) : DomainEvent(publisherId, type, context) { }

    public class TowDriverMedicalCertificateUpdated(string ownerName, int ownerAge, DateOnly issueDate, DateOnly expirationDate)
    {
        public readonly string MedicalCertificateOwnerName = ownerName;
        public readonly int MedicalCertificateOwnerAge = ownerAge;
        public readonly DateOnly MedicalCertificateIssueDate = issueDate;
        public readonly DateOnly MedicalCertificateExpirationDate = expirationDate;

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

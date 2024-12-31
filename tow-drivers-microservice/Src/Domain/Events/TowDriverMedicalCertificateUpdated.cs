using Application.Core;

namespace TowDrivers.Domain 
{
    public class TowDriverMedicalCertificateUpdatedEvent(string publisherId, string type, TowDriverMedicalCertificateUpdated context) : DomainEvent(publisherId, type, context) { }

    public class TowDriverMedicalCertificateUpdated(string ownerName, int ownerAge, DateTime issueDate, DateTime expirationDate)
    {
        private readonly string OwnerName = ownerName;
        private readonly int OwnerAge = ownerAge;
        private readonly DateTime IssueDate = issueDate;
        private readonly DateTime ExpirationDate = expirationDate;

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

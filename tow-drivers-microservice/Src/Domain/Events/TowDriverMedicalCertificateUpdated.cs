using Application.Core;

namespace TowDrivers.Domain 
{
    public class TowDriverMedicalCertificateUpdatedEvent(string publisherId, string type, TowDriverMedicalCertificateUpdated context) : DomainEvent(publisherId, type, context) { }

    public class TowDriverMedicalCertificateUpdated(string ownerName, int ownerAge, DateOnly issueDate, DateOnly expirationDate)
    {
        private readonly string OwnerName = ownerName;
        private readonly int OwnerAge = ownerAge;
        private readonly DateOnly IssueDate = issueDate;
        private readonly DateOnly ExpirationDate = expirationDate;

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

using Application.Core;

namespace TowDrivers.Domain
{ 
    public class TowDriverDrivingLicenseUpdatedEvent(string publisherId, string type, TowDriverDrivingLicenseUpdated context) : DomainEvent(publisherId, type, context) { }

    public class TowDriverDrivingLicenseUpdated(string ownerName, DateTime issueDate, DateTime expirationDate)
    {
        private readonly string OwnerName = ownerName;
        private readonly DateTime IssueDate = issueDate;
        private readonly DateTime ExpirationDate = expirationDate;  

        public static TowDriverDrivingLicenseUpdatedEvent CreateEvent(TowDriverId publisherId, TowDriverDrivingLicense drivingLicense)
        {
            return new TowDriverDrivingLicenseUpdatedEvent(
                publisherId.GetValue(),
                typeof(TowDriverDrivingLicenseUpdated).Name,
                new TowDriverDrivingLicenseUpdated(
                    drivingLicense.GetOwnerName(),
                    drivingLicense.GetIssueDate(),
                    drivingLicense.GetExpirationDate()
                )
            );
        }
    }
}

using Application.Core;

namespace TowDriver.Domain
{
    public class TowDriverDrivingLicenseUpdatedEvent(string publisherId, string type, TowDriverDrivingLicenseUpdated context) : DomainEvent(publisherId, type, context) { }

    public class TowDriverDrivingLicenseUpdated(string ownerName, DateOnly issueDate, DateOnly expirationDate)
    {
        public readonly string LicenseOwnerName = ownerName;
        public readonly DateOnly LicenseIssueDate = issueDate;
        public readonly DateOnly LicenseExpirationDate = expirationDate;  

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

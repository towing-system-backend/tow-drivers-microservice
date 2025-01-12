using Application.Core;

namespace TowDriver.Domain
{
    public class InvalidTowDriverDrivingLicenseDatesException : DomainException
    {
        public InvalidTowDriverDrivingLicenseDatesException() : base("The issue date must be before the expiration date.") { }
    }
}

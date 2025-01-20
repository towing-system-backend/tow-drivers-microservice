using Application.Core;

namespace TowDriver.Domain
{
    public class InvalidTowDriverMedicalCertificateDatesException : DomainException
    {
        public InvalidTowDriverMedicalCertificateDatesException() : base("The issue date must be before the expiration date.") { }
    }
}

using Application.Core;

namespace TowDrivers.Domain
{
    public class InvalidTowDriverMedicalCertificateDatesException : DomainException
    {
        public InvalidTowDriverMedicalCertificateDatesException() : base("The issue date must be before the expiration date.") { }
    }
}

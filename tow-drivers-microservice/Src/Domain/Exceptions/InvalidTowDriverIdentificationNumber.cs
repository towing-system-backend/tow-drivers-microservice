using Application.Core;

namespace TowDrivers.Domain
{
    public class InvalidTowDriverIdentificationNumberException : DomainException
    {
        public InvalidTowDriverIdentificationNumberException() : base("Invalid Tow Driver Identification Number.") { }
    }
}

using Application.Core;

namespace TowDriver.Domain
{
    public class InvalidTowDriverIdentificationNumberException : DomainException
    {
        public InvalidTowDriverIdentificationNumberException() : base("Invalid Tow Driver Identification Number.") { }
    }
}

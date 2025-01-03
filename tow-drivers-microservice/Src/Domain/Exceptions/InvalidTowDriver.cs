using Application.Core;

namespace TowDrivers.Domain
{
    public class InvalidTowDriverException : DomainException
    {
        public InvalidTowDriverException() : base("Invalid Tow Driver.") { }
    }
}

using Application.Core;

namespace TowDrivers.Domain
{
    public class InvalidTowDriverNameException : DomainException
    {
        public InvalidTowDriverNameException() : base("Invalid Tow Driver Name.") { }
    }
}

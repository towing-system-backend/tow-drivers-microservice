using Application.Core;

namespace TowDriver.Domain
{
    public class InvalidTowDriverNameException : DomainException
    {
        public InvalidTowDriverNameException() : base("Invalid Tow Driver Name.") { }
    }
}

using Application.Core;

namespace TowDriver.Domain
{
    public class InvalidTowDriverEmailException : DomainException
    {
        public InvalidTowDriverEmailException() : base("Invalid Tow Driver Email.") { }
    }
}

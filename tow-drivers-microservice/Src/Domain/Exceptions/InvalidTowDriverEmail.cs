using Application.Core;

namespace TowDrivers.Domain
{
    public class InvalidTowDriverEmailException : DomainException
    {
        public InvalidTowDriverEmailException() : base("Invalid Tow Driver Email.") { }
    }
}

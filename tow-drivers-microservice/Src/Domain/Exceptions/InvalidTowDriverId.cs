using Application.Core;

namespace TowDrivers.Domain
{
    public class InvalidTowDriverIdException : DomainException
    {
        public InvalidTowDriverIdException() : base("Invalid Tow Driver Id.") { }
    }
}

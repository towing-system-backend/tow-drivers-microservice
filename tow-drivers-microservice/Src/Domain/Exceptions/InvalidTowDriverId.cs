using Application.Core;

namespace TowDriver.Domain
{
    public class InvalidTowDriverIdException : DomainException
    {
        public InvalidTowDriverIdException() : base("Invalid Tow Driver Id.") { }
    }
}

using Application.Core;

namespace TowDriver.Domain
{
    public class InvalidTowDriverLocationException : DomainException
    {
        public InvalidTowDriverLocationException() : base("Invalid Tow Driver Location.") { }
    }
}

using Application.Core;

namespace TowDrivers.Domain
{
    public class InvalidTowDriverLocationException : DomainException
    {
        public InvalidTowDriverLocationException() : base("Invalid Tow Driver Location.") { }
    }
}

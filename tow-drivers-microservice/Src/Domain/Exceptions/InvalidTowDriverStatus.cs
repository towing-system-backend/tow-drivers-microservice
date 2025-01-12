using Application.Core;

namespace TowDriver.Domain
{
    public class InvalidTowDriverStatusException : DomainException
    {
        public InvalidTowDriverStatusException() : base("Invalid Tow Driver Status.") { }
    }
}

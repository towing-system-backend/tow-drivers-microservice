using Application.Core;

namespace TowDriver.Domain
{
    public class InvalidTowDriverException : DomainException
    {
        public InvalidTowDriverException() : base("Invalid Tow Driver.") { }
    }
}

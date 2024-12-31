using Application.Core;

namespace TowDrivers.Domain
{
    public class InvalidTowDriverStatusException : DomainException
    {
        public InvalidTowDriverStatusException() : base("Invalid Tow Driver Status.")
        {
        }
    }
}

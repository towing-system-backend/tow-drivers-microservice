using Application.Core;

namespace TowDrivers.Domain
{
    public class InvalidTowDriverOwnerAgeException : DomainException
    {
        public InvalidTowDriverOwnerAgeException() : base("Owner age must be greater than 15.") { }
    }
}

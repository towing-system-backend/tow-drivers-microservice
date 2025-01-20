using Application.Core;

namespace TowDriver.Domain
{
    public class InvalidSupplierCompanyIdException : DomainException
    {
        public InvalidSupplierCompanyIdException() : base("Invalid supplier company id.") { }
    }
}
using Application.Core;

namespace TowDriver.Application
{
    public class TowDriverAlreadyExists : ApplicationError
    {
        public TowDriverAlreadyExists() : base("Tow driver already exist.") { }
    }
}
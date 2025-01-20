using Application.Core;

namespace TowDriver.Application
{
    public class TowDriversNotFound : ApplicationError
    {
        public TowDriversNotFound() : base("Tow drivers not found.") { }
    }
}

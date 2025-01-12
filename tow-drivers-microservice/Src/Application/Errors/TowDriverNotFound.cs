using Application.Core;

namespace TowDriver.Application
{
    public class TowDriverNotFound : ApplicationError
    {
        public TowDriverNotFound() : base("Tow driver not found.") { }
    }
}

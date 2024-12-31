using Application.Core;

namespace TowDrivers.Domain
{
    public class TowDriverAlredyExistError : ApplicationError
    {
        public TowDriverAlredyExistError() : base("Tow driver already exist"){}
    }
}

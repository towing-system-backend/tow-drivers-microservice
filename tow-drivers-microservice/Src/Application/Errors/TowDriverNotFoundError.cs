namespace TowDrivers.Domain
{
    public class TowDriverNotFoundError : ApplicationException
    {
        public TowDriverNotFoundError() : base("Tow driver not found."){}
    }
}

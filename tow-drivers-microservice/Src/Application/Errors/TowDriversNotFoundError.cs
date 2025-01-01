namespace TowDrivers.Domain
{
    public class TowDriversNotFoundError : ApplicationException
    {
        public TowDriversNotFoundError() : base("Tow drivers not found."){}
    }
}

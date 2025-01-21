namespace Application.Core
{
    using TowDriver.Domain;
    public interface ISupplierCompanyRespository
    {
        Task SaveTowDriver(TowDriver driver);  
    }
}

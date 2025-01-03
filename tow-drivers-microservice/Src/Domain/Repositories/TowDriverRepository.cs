using Application.Core;

namespace TowDrivers.Domain
{
    public interface ITowDriverRepository
    {
        Task<Optional<TowDriver>> FindById(string towDriverId);
        Task<Optional<TowDriver>> FindByEmail(string email);
        Task Save(TowDriver towDriver);
        Task Remove(string towDriverId);
    }
}

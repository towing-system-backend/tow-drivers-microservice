using Application.Core;

namespace TowDriver.Domain
{
    public interface ITowDriverRepository
    {
        Task<Optional<TowDriver>> FindById(string towDriverId);
        Task<Optional<TowDriver>> FindByEmail(string email);
        Task Save(TowDriver towDriver);
    }
}
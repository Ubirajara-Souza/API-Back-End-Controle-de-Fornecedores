using Bira.App.Providers.Domain.Entities;

namespace Bira.App.Providers.Service.Interfaces
{
    public interface IProviderService : IDisposable
    {
        Task Add(Provider provider);
        Task Update(Provider provider);
        Task Delete(Guid id);
        Task UpdateAddress(Address address);
    }
}

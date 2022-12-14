using Bira.App.Providers.Domain.Entities;
using Bira.App.Providers.Domain.Package;

namespace Bira.App.Providers.Domain.Interfaces.Repositories
{
    public interface IAddressRepository : IRepository<Address>
    {
        Task<Address> GetAddressPerProvider(Guid providerId);
    }
}

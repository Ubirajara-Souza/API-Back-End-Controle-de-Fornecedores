using Bira.App.Providers.Domain.Entities;
using Bira.App.Providers.Domain.Package;

namespace Bira.App.Providers.Domain.Interfaces.Repositories
{
    public interface IProviderRepository : IRepository<Provider>
    {
        Task<Provider> GetProviderAddress(Guid id);
        Task<Provider> GetProviderProductAddress(Guid id);
    }
}

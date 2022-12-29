using Bira.App.Providers.Domain.Entities;
using Bira.App.Providers.Domain.Package;

namespace Bira.App.Providers.Domain.Interfaces.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductPerProvider(Guid providerId);
        Task<IEnumerable<Product>> GetProductsProviders();
        Task<Product> GetProductProviderById(Guid id);
    }
}

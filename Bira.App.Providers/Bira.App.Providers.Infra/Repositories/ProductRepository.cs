using Bira.App.Providers.Domain.Entities;
using Bira.App.Providers.Domain.Interfaces.Repositories;
using Bira.App.Providers.Infra.Repositories.BaseContext;
using Microsoft.EntityFrameworkCore;

namespace Bira.App.Providers.Infra.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApiDbContext _context) : base(_context) { }
        public async Task<Product> GetProductProviderById(Guid id)
        {
            return await Context.Products.AsNoTracking().Include(p => p.Provider)
                .FirstOrDefaultAsync(prod => prod.Id == id);

        }
        public async Task<IEnumerable<Product>> GetProductsProviders()
        {
            return await Context.Products.AsNoTracking().Include(p => p.Provider)
                .OrderBy(prod => prod.Name).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductPerProvider(Guid providerId)
        {
            return await Search(p => p.ProviderId == providerId);

        }
    }
}
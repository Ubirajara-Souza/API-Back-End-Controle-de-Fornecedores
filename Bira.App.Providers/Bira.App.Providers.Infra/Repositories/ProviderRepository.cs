using Bira.App.Providers.Domain.Entities;
using Bira.App.Providers.Domain.Interfaces.Repositories;
using Bira.App.Providers.Infra.Repositories.BaseContext;
using Microsoft.EntityFrameworkCore;

namespace Bira.App.Providers.Infra.Repositories
{
    public class ProviderRepository : Repository<Provider>, IProviderRepository
    {
        public ProviderRepository(ApiDbContext _context) : base(_context) { }
        public async Task<Provider> GetProviderAddress(Guid id)
        {
            return await Context.Providers.AsNoTracking().Include(p => p.Address)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Provider> GetProviderProductAddress(Guid id)
        {
            return await Context.Providers.AsNoTracking()
                .Include(p => p.Products)
                .Include(p => p.Address)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}

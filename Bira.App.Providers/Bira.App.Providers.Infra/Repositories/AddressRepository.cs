using Bira.App.Providers.Domain.Entities;
using Bira.App.Providers.Domain.Interfaces.Repositories;
using Bira.App.Providers.Infra.Repositories.BaseContext;
using Microsoft.EntityFrameworkCore;

namespace Bira.App.Providers.Infra.Repositories
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(ApiDbContext _context) : base(_context) { }
        public async Task<Address> GetAddressPerProvider(Guid providerId)
        {
            return await Context.Addresses.AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProviderId == providerId);

        }
    }
}
using Bira.App.Providers.Domain.Entities;

namespace Bira.App.Providers.Service.Interfaces
{
    public interface IProductService : IDisposable
    {
        Task Add(Product product);
        Task Update(Product product);
        Task Delete(Guid id);
    }
}

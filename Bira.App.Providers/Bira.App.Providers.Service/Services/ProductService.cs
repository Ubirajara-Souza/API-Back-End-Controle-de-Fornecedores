using Bira.App.Providers.Application.Validators;
using Bira.App.Providers.Domain.Entities;
using Bira.App.Providers.Domain.Interfaces.Repositories;
using Bira.App.Providers.Service.Interfaces;

namespace Bira.App.Providers.Service.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository, INotifier notifier) : base(notifier)
        {
            _productRepository = productRepository;
        }

        public async Task Update(Product product)
        {
            if (!RunValidation(new ProductValidators(), product)) return;
            await _productRepository.Update(product);
        }

        public async Task Delete(Guid id)
        {
            await _productRepository.Delete(id);
        }

        public void Dispose()
        {
            _productRepository.Dispose();
        }
    }
}

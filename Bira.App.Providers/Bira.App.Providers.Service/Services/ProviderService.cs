using Bira.App.Providers.Application.Validators;
using Bira.App.Providers.Domain.Entities;
using Bira.App.Providers.Domain.Interfaces.Repositories;
using Bira.App.Providers.Service.Interfaces;

namespace Bira.App.Providers.Service.Services
{
    public class ProviderService : BaseService, IProviderService
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IAddressRepository _addressRepository;

        public ProviderService(IProviderRepository providerRepository,
            IAddressRepository addressRepository, INotifier notifier) : base(notifier)
        {
            _providerRepository = providerRepository;
            _addressRepository = addressRepository;
        }

        public async Task Add(Provider provider)
        {
            if (!RunValidation(new ProviderValidators(), provider)
                || !RunValidation(new AddressValidators(), provider.Address)) return;

            if (_providerRepository.Search(f => f.Document == provider.Document).Result.Any())
            {
                Notify("Já existe um fornecedor com este documento informado.");
                return;
            }

            await _providerRepository.Add(provider);
        }

        public async Task Update(Provider provider)
        {
            if (!RunValidation(new ProviderValidators(), provider)) return;

            if (_providerRepository.Search(f => f.Document == provider.Document && f.Id != provider.Id).Result.Any())
            {
                Notify("Já existe um fornecedor com este documento infomado.");
                return;
            }

            await _providerRepository.Update(provider);
        }

        public async Task UpdateAddress(Address address)
        {
            if (!RunValidation(new AddressValidators(), address)) return;
            await _addressRepository.Update(address);
        }

        public async Task Delete(Guid id)
        {
            if (_providerRepository.GetProviderProductAddress(id).Result.Products.Any())
            {
                Notify("O fornecedor possui produtos cadastrados!");
                return;
            }

            var address = await _addressRepository.GetAddressPerProvider(id);

            if (address != null)
            {
                await _addressRepository.Delete(address.Id);
            }

            await _providerRepository.Delete(id);
        }

        public void Dispose()
        {
            _providerRepository.Dispose();
            _addressRepository.Dispose();
        }
    }
}

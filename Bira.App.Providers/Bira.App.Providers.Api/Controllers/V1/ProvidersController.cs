﻿using AutoMapper;
using Bira.App.Providers.Domain.DTOs.Request;
using Bira.App.Providers.Domain.Entities;
using Bira.App.Providers.Domain.Extensions;
using Bira.App.Providers.Domain.Interfaces;
using Bira.App.Providers.Domain.Interfaces.Repositories;
using Bira.App.Providers.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bira.App.Providers.Api.Controllers.V1
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProvidersController : BaseController
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IProviderService _providerService;
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public ProvidersController(IProviderRepository providerRepository, IProviderService providerService,
            IAddressRepository addressRepository, IMapper mapper, INotifier notifier, IUser user) : base(notifier, user)
        {
            _providerRepository = providerRepository;
            _providerService = providerService;
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<ProviderDto>> GetAllProvider()
        {
            var provider = _mapper.Map<IEnumerable<ProviderDto>>(await _providerRepository.GetAll());
            return provider;
        }

        [ClaimsAuthorize("Provider", "GetByID")]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProviderDto>> GetProviderById(Guid id)
        {
            var provider = await GetProviderProductAddress(id);

            if (provider == null) return NotFound();
            return provider;
        }

        [ClaimsAuthorize("Provider", "Add")]
        [HttpPost]
        public async Task<ActionResult<ProviderDto>> AddProvider(ProviderDto providerDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var provider = _mapper.Map<Provider>(providerDto);
            await _providerService.Add(provider);

            return CustomResponse(providerDto);
        }

        [ClaimsAuthorize("Provider", "Update")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ProviderDto>> UpdateProvider(Guid id, ProviderDto providerDto)
        {
            if (id != providerDto.Id)
            {
                NotifyError("Os ids informados não são iguais!");
                return CustomResponse(ModelState);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var provider = _mapper.Map<Provider>(providerDto);
            await _providerService.Update(provider);

            return CustomResponse(providerDto);
        }

        [ClaimsAuthorize("Provider", "Delete")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ProviderDto>> DeleteProvider(Guid id)
        {
            var providerDto = await GetProviderAddress(id);

            if (providerDto == null) return NotFound();

            await _providerService.Delete(id);

            return CustomResponse(providerDto);
        }

        [ClaimsAuthorize("Provider", "GetByID")]
        [HttpGet("address/{id:guid}")]
        public async Task<AddressDto> GetAddressById(Guid id)
        {
            var address = await _addressRepository.GetById(id);
            var addressDto = _mapper.Map<AddressDto>(address);
            return addressDto;
        }

        [ClaimsAuthorize("Provider", "Update")]
        [HttpPut("address/{id:guid}")]
        public async Task<IActionResult> UpdateAddress(Guid id, AddressDto addressDto)
        {
            if (id != addressDto.Id)
            {
                NotifyError("Os ids informados não são iguais!");
                return CustomResponse(addressDto);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var address = _mapper.Map<Address>(addressDto);
            await _providerService.UpdateAddress(address);

            return CustomResponse(addressDto);
        }

        private async Task<ProviderDto> GetProviderProductAddress(Guid id)
        {
            return _mapper.Map<ProviderDto>(await _providerRepository.GetProviderProductAddress(id));
        }

        private async Task<ProviderDto> GetProviderAddress(Guid id)
        {
            return _mapper.Map<ProviderDto>(await _providerRepository.GetProviderAddress(id));
        }
    }
}

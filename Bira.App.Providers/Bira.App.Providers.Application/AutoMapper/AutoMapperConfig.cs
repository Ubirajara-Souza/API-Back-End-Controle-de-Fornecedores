using AutoMapper;
using Bira.App.Providers.Domain.DTOs;
using Bira.App.Providers.Domain.Entities;

namespace Bira.App.Providers.Application.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Provider, ProviderDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}

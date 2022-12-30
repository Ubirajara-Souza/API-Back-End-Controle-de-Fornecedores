using AutoMapper;
using Bira.App.Providers.Domain.DTOs.Request;
using Bira.App.Providers.Domain.Entities;

namespace Bira.App.Providers.Application.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Provider, ProviderDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<ProductDto, Product>();

            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.NameProvider, opt => opt.MapFrom(src => src.Provider.Name));
        }
    }
}

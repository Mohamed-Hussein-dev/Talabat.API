using AutoMapper;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;

namespace Talabat.APIs.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() {
            CreateMap<Product, ProductToReturn>()
                     .ForMember(PTR => PTR.ProductBrand, OP => OP.MapFrom(S => S.ProductBrand.Name))
                     .ForMember(PTR => PTR.ProductType, OP => OP.MapFrom(S => S.ProductType.Name))
                     .ForMember(PTR => PTR.PictureUrl, OP => OP.MapFrom<PictureURLResolver>());

            CreateMap<Address, UserAddressDto>().ReverseMap();

            CreateMap<CustomerBasketDto,CustomerBasket>();

            CreateMap<BasketItemDto, BasketItem>();
                     
        }
    }
}

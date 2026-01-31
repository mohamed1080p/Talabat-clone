
using AutoMapper;
using Domain.Models;
using Shared.DataTransferedObjects;

namespace Service.MappingProfiles
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(dist => dist.BrandName, options => options.MapFrom(src => src.productBrand.Name))
                .ForMember(dist => dist.TypeName, options => options.MapFrom(src => src.productType.Name));

            CreateMap<ProductType, TypeDTO>();
            CreateMap<ProductBrand, BrandDTO>();
        }
    }
}

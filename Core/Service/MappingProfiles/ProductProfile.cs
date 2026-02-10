
using AutoMapper;
using Domain.Models.ProductModule;
using Shared.DataTransferedObjects.ProductModuleDTOs;

namespace Service.MappingProfiles
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(dist => dist.BrandName, options => options.MapFrom(src => src.productBrand.Name))
                .ForMember(dist => dist.TypeName, options => options.MapFrom(src => src.productType.Name))
                .ForMember(dist => dist.PictureUrl, options => options.MapFrom<PictureUrlResolver>());

            CreateMap<ProductType, TypeDTO>();
            CreateMap<ProductBrand, BrandDTO>();
        }
    }
}

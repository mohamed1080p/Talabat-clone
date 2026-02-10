using AutoMapper;
using Domain.Models.ProductModule;
using Microsoft.Extensions.Configuration;
using Shared.DataTransferedObjects.ProductModuleDTOs;

namespace Service.MappingProfiles
{
    public class PictureUrlResolver(IConfiguration _configuration) : IValueResolver<Product, ProductDTO, string>
    {
        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if(string.IsNullOrEmpty(source.PictureUrl))
            {
                return string.Empty;
            }
            else
            {
                var Url = $"{_configuration.GetSection("Urls")["BaseUrl"]}{source.PictureUrl}";
                return Url;
            }
                
        }
    }
}

using Domain.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    internal class ProductCountSpecification:BaseSpecifications<Product, int>
    {
        public ProductCountSpecification(ProductQueryParams queryParams) 
            :base(a=> (!queryParams.BrandId.HasValue || a.BrandId== queryParams.BrandId)
            && (!queryParams.TypeId.HasValue || a.TypeId== queryParams.TypeId) 
            && (string.IsNullOrWhiteSpace(queryParams.SearchValue) || a.Name.ToLower().Contains(queryParams.SearchValue.ToLower())))
        {
            

        }
    }
}

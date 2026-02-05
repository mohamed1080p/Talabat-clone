
using Domain.Models;
using Shared;

namespace Service.Specifications
{
    internal class ProductWithBrandAndTypeSpecifications:BaseSpecifications<Product,int>
    {
        public ProductWithBrandAndTypeSpecifications(ProductQueryParams queryParams):
            base(a=> (!queryParams.BrandId.HasValue || a.BrandId== queryParams.BrandId)
            && (!queryParams.TypeId.HasValue || a.TypeId== queryParams.TypeId))
        {
            AddInclude(a => a.productBrand);
            AddInclude(a => a.productType);
            switch (queryParams.sortingOptions)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(a => a.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDesc(a => a.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(a => a.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDesc(a => a.Price);
                    break;
                default:
                    break;

            }
        }

        public ProductWithBrandAndTypeSpecifications(int id):base(a=>a.Id==id)
        {
            AddInclude(a => a.productBrand);
            AddInclude(a => a.productType);
        }
    }
}

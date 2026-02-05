
using Domain.Models;
using Shared;

namespace Service.Specifications
{
    internal class ProductWithBrandAndTypeSpecifications:BaseSpecifications<Product,int>
    {
        public ProductWithBrandAndTypeSpecifications(int? BrandId, int? TypeId, ProductSortingOptions sortingOptions):
            base(a=> (!BrandId.HasValue || a.BrandId==BrandId) && (!TypeId.HasValue || a.TypeId==TypeId))
        {
            AddInclude(a => a.productBrand);
            AddInclude(a => a.productType);
            switch (sortingOptions)
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

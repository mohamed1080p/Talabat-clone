
using Domain.Models;

namespace Service.Specifications
{
    internal class ProductWithBrandAndTypeSpecifications:BaseSpecifications<Product,int>
    {
        public ProductWithBrandAndTypeSpecifications(int? BrandId, int? TypeId):
            base(a=> (!BrandId.HasValue || a.BrandId==BrandId) && (!TypeId.HasValue || a.TypeId==TypeId))
        {
            AddInclude(a => a.productBrand);
            AddInclude(a => a.productType);
        }

        public ProductWithBrandAndTypeSpecifications(int id):base(a=>a.Id==id)
        {
            AddInclude(a => a.productBrand);
            AddInclude(a => a.productType);
        }
    }
}


namespace Shared
{
    public class ProductQueryParams
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public ProductSortingOptions sortingOptions { get; set; }
    }
}

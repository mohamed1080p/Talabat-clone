namespace Domain.Models.ProductModule
{
    public class Product:BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; }=string.Empty;
        public string PictureUrl { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public ProductBrand productBrand { get; set; }
        public int BrandId { get; set; }
        public ProductType productType { get; set; }
        public int TypeId { get; set; }


    }
}

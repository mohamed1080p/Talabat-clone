using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferedObjects.BasketModuleDTOs
{
    public class BasketItemDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string PictureUrl { get; set; } = string.Empty;
        [Range(1, double.MaxValue)]
        public decimal Price { get; set; }
        [Range(1,100)]
        public int Quantity { get; set; }
    }
}
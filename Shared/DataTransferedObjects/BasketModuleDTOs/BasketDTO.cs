
namespace Shared.DataTransferedObjects.BasketModuleDTOs
{
    public class BasketDTO
    {
        public string Id { get; set; }
        public ICollection<BasketItemDTO> Items { get; set; } = [];
    }
}

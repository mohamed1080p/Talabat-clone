
using Shared.DataTransferedObjects.IdentityDTOs;

namespace Shared.DataTransferedObjects.OrderDTOs
{
    public class OrderToReturnDTO
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; } = default!;
        public DateTimeOffset OrderDate { get; set; }
        public AddressDTO Address { get; set; } = default!;
        public string DeliveryMethod { get; set; } = default!;
        public string OrderStatus { get; set; } = default!;
        public ICollection<OrderItemDTO> Items { get; set; } = [];
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }

    }
}


using Shared.DataTransferedObjects.IdentityDTOs;

namespace Shared.DataTransferedObjects.OrderDTOs
{
    public class OrderDTO
    {
        public string BasketId { get; set; } = default!;
        public int DeliveryMethodId { get; set; }
        public AddressDTO Address { get; set; } = default!;
    }
}

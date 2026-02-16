
using Shared.DataTransferedObjects.OrderDTOs;

namespace ServicesAbstraction
{
    public interface IOrderService
    {
        Task<OrderToReturnDTO> CreateOrderAsync(OrderDTO orderDTO, string Email);

        // get delivery methods
        Task<IEnumerable<DeliveryMethodDTO>> GetDeliveryMethodsAsync();

        Task<IEnumerable<OrderToReturnDTO>> GetAllOrdersAsync(string Email);

        Task<OrderToReturnDTO> GetOrderByIdAsync(Guid Id);



    }
}

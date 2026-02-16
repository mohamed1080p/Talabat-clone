
using Shared.DataTransferedObjects.OrderDTOs;

namespace ServicesAbstraction
{
    public interface IOrderService
    {
        Task<OrderToReturnDTO> CreateOrder(OrderDTO orderDTO, string Email);

    }
}

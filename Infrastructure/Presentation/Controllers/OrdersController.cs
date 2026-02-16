
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferedObjects.OrderDTOs;
using System.Net.Mail;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Authorize]
    public class OrdersController(IServiceManager _serviceManager):ApiBaseController
    {
        // create order
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDTO>> CreateOrder(OrderDTO orderDTO)
        {
            var Email = GetEmailFromToken();
            var Order = await _serviceManager.OrderService.CreateOrderAsync(orderDTO, Email!);
            return Ok(Order);
        }

        // get delivery methods
        [AllowAnonymous]
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodDTO>>> GetDeliveryMethods()
        {
            var DeliveryMethods = await _serviceManager.OrderService.GetDeliveryMethodsAsync();
            return Ok(DeliveryMethods);
        }

        // get all orders by email
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturnDTO>>> GetAllOrders()
        {
            var Orders = await _serviceManager.OrderService.GetAllOrdersAsync(GetEmailFromToken());
            return Ok(Orders);
        }

        // get order by id
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderToReturnDTO>> GetOrderById(Guid id)
        {
            var Order = await _serviceManager.OrderService.GetOrderByIdAsync(id);
            return Ok(Order);
        }
    }
}

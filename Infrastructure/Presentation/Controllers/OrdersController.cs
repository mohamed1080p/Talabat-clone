
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferedObjects.OrderDTOs;
using System.Net.Mail;
using System.Security.Claims;

namespace Presentation.Controllers
{
    public class OrdersController(IServiceManager _serviceManager):ApiBaseController
    {
        // create order
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDTO>> CreateOrder(OrderDTO orderDTO)
        {
            var Email = GetEmailFromToken();
            var Order = _serviceManager.OrderService.CreateOrder(orderDTO, Email!);
            return Ok(Order);
        }

        // get delivery methods
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodDTO>>> GetDeliveryMethods()
        {
            
        }

        // get all orders by email


        // get order by id
    }
}

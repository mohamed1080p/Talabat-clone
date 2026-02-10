
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferedObjects.BasketModuleDTOs;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BasketController(IServiceManager _serviceManager):ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<BasketDTO>> GetBasket(string id)
        {
            var Basket = await _serviceManager.BasketService.GetBasketAsync(id);
            return Ok(Basket);
        }

        [HttpPost]
        public async Task<ActionResult<BasketDTO>> CreateOrUpdateBasket(BasketDTO basket)
        {
            var Basket = await _serviceManager.BasketService.CreateOrUpdateBasketAsync(basket);
            return Ok(Basket);
        }

        [HttpDelete("{Key}")]
        public async Task<ActionResult<bool>> DeleteBasket(string key)
        {
            var Result = await _serviceManager.BasketService.DeleteBasketAsync(key);
            return Ok(Result);
        }
    }
}

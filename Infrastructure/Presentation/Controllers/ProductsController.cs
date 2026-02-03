
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferedObjects;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductsController(IServiceManager _serviceManager):ControllerBase
    {
        // get all products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts(int? BrandId, int? TypeId)
        {
            var products = await _serviceManager.ProductService.GetAllProductsAsync(BrandId, TypeId);
            return Ok(products);
        }

        //get product by Id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            var product = await _serviceManager.ProductService.GetProductByIdAsync(id);
            return Ok(product);
        }

        // get all types
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeDTO>>> GetTypes()
        {
            var type = await _serviceManager.ProductService.GetAllTypesAsync();
            return Ok(type);
        }

        // get all brands
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandDTO>>> GetBrands()
        {
            var brands = await _serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(brands);
        }

    }
}

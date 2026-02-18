
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;
using ServiceAbstraction;
using Shared;
using Shared.DataTransferedObjects.ProductModuleDTOs;

namespace Presentation.Controllers
{
    public class ProductsController(IServiceManager _serviceManager) : ApiBaseController
    {
        // get all products

        [HttpGet]
        [Cache]
        public async Task<ActionResult<PaginatedResult<ProductDTO>>> GetAllProducts([FromQuery]ProductQueryParams queryParams)
        {
            var products = await _serviceManager.ProductService.GetAllProductsAsync(queryParams);
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
        [Cache]
        public async Task<ActionResult<IEnumerable<TypeDTO>>> GetTypes()
        {
            var type = await _serviceManager.ProductService.GetAllTypesAsync();
            return Ok(type);
        }

        // get all brands
        [HttpGet("brands")]
        [Cache]
        public async Task<ActionResult<IEnumerable<BrandDTO>>> GetBrands()
        {
            var brands = await _serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(brands);
        }

    }
}


using Shared.DataTransferedObjects;

namespace ServiceAbstraction
{
    public interface IProductService
    {
        // get all products
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();

        // get product by Id
        Task<ProductDTO> GetProductByIdAsync(int Id);

        // get all types
        Task<IEnumerable<TypeDTO>> GetAllTypesAsync();

        // get all brands
        Task<IEnumerable<BrandDTO>> GetAllBrandsAsync();
    }
}

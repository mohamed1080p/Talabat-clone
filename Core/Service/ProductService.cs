
using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using ServiceAbstraction;
using Shared.DataTransferedObjects;

namespace Service
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDTO>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDTO>>(brands);
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
        }

        public async Task<IEnumerable<TypeDTO>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDTO>>(types);
        }

        public async Task<ProductDTO> GetProductByIdAsync(int Id)
        {
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(Id);
            return _mapper.Map<Product, ProductDTO>(product);
        }
    }
}


using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Service.Specifications;
using ServiceAbstraction;
using Shared;
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

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync(int? BrandId, int? TypeId, ProductSortingOptions sortingOptions)
        {
            var specifications = new ProductWithBrandAndTypeSpecifications(BrandId, TypeId, sortingOptions);
            var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(specifications);
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
        }

        public async Task<IEnumerable<TypeDTO>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDTO>>(types);
        }

        public async Task<ProductDTO> GetProductByIdAsync(int Id)
        {
            var specifications = new ProductWithBrandAndTypeSpecifications(Id);
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(specifications);
            return _mapper.Map<Product, ProductDTO>(product);
        }
    }
}

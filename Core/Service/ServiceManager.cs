using AutoMapper;
using Domain.Contracts;
using ServiceAbstraction;

namespace Service
{
    public class ServiceManager(IUnitOfWork _unitOfWork, IMapper _mapper) : IServiceManager
    {
        private readonly Lazy<IProductService> _LazyProductService = new(() => new ProductService(_unitOfWork, _mapper));
        public IProductService ProductService => _LazyProductService.Value;
    }
}

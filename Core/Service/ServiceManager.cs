using AutoMapper;
using Domain.Contracts;
using ServiceAbstraction;

namespace Service
{
    public class ServiceManager(IUnitOfWork _unitOfWork, IMapper _mapper, IBasketRepository basketRepository) : IServiceManager
    {
        private readonly Lazy<IProductService> _LazyProductService = new(() => new ProductService(_unitOfWork, _mapper));
        public IProductService ProductService => _LazyProductService.Value;

        private readonly Lazy<IBasketService> _LazyBasketService = new Lazy<IBasketService>(() => new BasketService(basketRepository, _mapper));
        public IBasketService BasketService => _LazyBasketService.Value;
    }
}

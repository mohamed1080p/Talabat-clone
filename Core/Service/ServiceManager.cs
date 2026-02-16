using AutoMapper;
using Domain.Contracts;
using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServiceAbstraction;
using Services;
using ServicesAbstraction;

namespace Service
{
    public class ServiceManager(IUnitOfWork _unitOfWork, IMapper _mapper, IBasketRepository basketRepository, UserManager<ApplicationUser> _userManager, IConfiguration _configuration) : IServiceManager
    {
        private readonly Lazy<IProductService> _LazyProductService = new(() => new ProductService(_unitOfWork, _mapper));
        public IProductService ProductService => _LazyProductService.Value;

        private readonly Lazy<IBasketService> _LazyBasketService = new Lazy<IBasketService>(() => new BasketService(basketRepository, _mapper));
        private readonly Lazy<IAuthenticationService> _LazyAuthenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(_userManager, _configuration,_mapper));
        public IBasketService BasketService => _LazyBasketService.Value;

        public IAuthenticationService AuthenticationService => _LazyAuthenticationService.Value;

        private readonly Lazy<IOrderService> _LazyOrderService = new Lazy<IOrderService>(() => new OrderService(_mapper, basketRepository, _unitOfWork));
        public IOrderService OrderService => _LazyOrderService.Value;
    }
}

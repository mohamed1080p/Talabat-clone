using AutoMapper;
using Domain.Contracts;
using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServiceAbstraction;

namespace Service
{
    public class ServiceManager(IUnitOfWork _unitOfWork, IMapper _mapper, IBasketRepository basketRepository, UserManager<ApplicationUser> _userManager, IConfiguration _configuration) : IServiceManager
    {
        private readonly Lazy<IProductService> _LazyProductService = new(() => new ProductService(_unitOfWork, _mapper));
        public IProductService ProductService => _LazyProductService.Value;

        private readonly Lazy<IBasketService> _LazyBasketService = new Lazy<IBasketService>(() => new BasketService(basketRepository, _mapper));
        private readonly Lazy<AuthenticationService> _LazyAuthenticationService = new Lazy<AuthenticationService>(() => new AuthenticationService(_userManager, _configuration));
        public IBasketService BasketService => _LazyBasketService.Value;

        public IAuthenticationService AuthenticationService => _LazyAuthenticationService.Value;
    }
}

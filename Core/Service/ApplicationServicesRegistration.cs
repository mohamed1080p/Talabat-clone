
using Microsoft.Extensions.DependencyInjection;
using Service.MappingProfiles;
using ServiceAbstraction;
using Services;
using Services.MappingProfiles;
using ServicesAbstraction;

namespace Service
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddAutoMapper(a => a.AddProfile(new ProductProfile()));
            Services.AddAutoMapper(a=>a.AddProfile(new BasketProfile()));
            Services.AddAutoMapper(a=>a.AddProfile(new IdentityProfile()));
            Services.AddAutoMapper(a=>a.AddProfile(new OrderProfile()));

            Services.AddScoped<IServiceManager, ServiceManagerWithFactoryDelegate>();

            Services.AddScoped<IProductService, ProductService>();
            Services.AddScoped<Func<IProductService>>(provider =>
                () => provider.GetRequiredService<IProductService>());

            Services.AddScoped<IOrderService, OrderService>();
            Services.AddScoped<Func<IOrderService>>(provider =>
                () => provider.GetRequiredService<IOrderService>());

            Services.AddScoped<IAuthenticationService, AuthenticationService>();
            Services.AddScoped<Func<IAuthenticationService>>(provider =>
                () => provider.GetRequiredService<IAuthenticationService>());

            Services.AddScoped<IBasketService, BasketService>();
            Services.AddScoped<Func<IBasketService>>(provider =>
                () => provider.GetRequiredService<IBasketService>());

            Services.AddScoped<ICacheService, CacheService>();

            return Services;
        }
    }
}

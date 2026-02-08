
using Microsoft.Extensions.DependencyInjection;
using Service.MappingProfiles;
using ServiceAbstraction;

namespace Service
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddAutoMapper(a => a.AddProfile(new ProductProfile()));
            Services.AddScoped<IServiceManager, ServiceManager>();
            return Services;
        }
    }
}

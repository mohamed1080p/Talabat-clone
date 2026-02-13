
using Domain.Contracts;
using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using persistence.Data;
using persistence.Identity;
using persistence.Repositories;
using StackExchange.Redis;

namespace persistence
{
    public static class InfrastructureServicesRegisteration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection Services, IConfiguration Configuration)
        {
            Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            Services.AddScoped<IDataSeeding, DataSeeding>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<IBasketRepository, BasketRepository>();
            Services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
                return ConnectionMultiplexer.Connect(Configuration.GetConnectionString("RedisConnectionString"));
            });

            Services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection"));
            });
            Services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<StoreIdentityDbContext>();

            return Services;
        }
    }
}


using Domain.Contracts;
using Domain.Models.IdentityModule;
using Domain.Models.ProductModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using persistence.Data;
using persistence.Identity;
using System.Text.Json;

namespace persistence
{
    public class DataSeeding(StoreDbContext _dbContext, 
        UserManager<ApplicationUser> _userManager, 
        RoleManager<IdentityRole> _roleManager,
        StoreIdentityDbContext _identityDbContext) : IDataSeeding
    {
        public async Task DataSeedAsync()
        {
			try
			{
                if ((await _dbContext.Database.GetPendingMigrationsAsync()).Any())
                {
                    await _dbContext.Database.MigrateAsync();
                }
                if (!_dbContext.ProductBrands.Any())
                {
                    var productbrand = File.OpenRead("..\\Infrastructure\\Presistence\\DataSeedFiles\\brands.json");
                    var brands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(productbrand);
                    if (brands is not null && brands.Any())
                    {
                       await _dbContext.ProductBrands.AddRangeAsync(brands);
                    }
                }

                if (!_dbContext.Products.Any())
                {
                    var products = File.OpenRead("..\\Infrastructure\\Presistence\\DataSeedFiles\\products.json");
                    var productsdata = await JsonSerializer.DeserializeAsync<List<Product>>(products);
                    if (productsdata is not null && productsdata.Any())
                    {
                        await _dbContext.Products.AddRangeAsync(productsdata);
                    }
                }

                if (!_dbContext.ProductTypes.Any())
                {
                    var productTypes = File.OpenRead("..\\Infrastructure\\Presistence\\DataSeedFiles\\types.json");
                    var types = await JsonSerializer.DeserializeAsync<List<ProductType>>(productTypes);
                    if (types is not null && types.Any())
                    {
                        await _dbContext.ProductTypes.AddRangeAsync(types);
                    }
                }
                await _dbContext.SaveChangesAsync();
            }
			catch (Exception)
			{

			}
        }

        public async Task IdentityDataSeedAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                if (!_userManager.Users.Any())
                {
                    var user01 = new ApplicationUser()
                    {
                        Email = "mohamed@gmail.com",
                        DisplayName = "Mohamed Bahaa",
                        PhoneNumber = "0123456789",
                        UserName = "mohamed1080p"
                    };
                    var user02 = new ApplicationUser()
                    {
                        Email = "ahmed@gmail.com",
                        DisplayName = "Ahmed",
                        PhoneNumber = "0123456789",
                        UserName = "ahmed1080p"
                    };
                    await _userManager.CreateAsync(user01, "P@ssw0rd");
                    await _userManager.CreateAsync(user02, "P@ssw0rd");
                    await _userManager.AddToRoleAsync(user01, "SuperAdmin");
                    await _userManager.AddToRoleAsync(user02, "Admin");
                }
                await _identityDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                
            }
        }
    }
}


using Domain.Contracts;
using Domain.Models.ProductModule;
using Microsoft.EntityFrameworkCore;
using persistence.Data;
using System.Text.Json;

namespace persistence
{
    public class DataSeeding(StoreDbContext _dbContext) : IDataSeeding
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
    }
}

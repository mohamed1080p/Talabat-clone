
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using persistence.Data;
using System.Text.Json;

namespace persistence
{
    public class DataSeeding(StoreDbContext _dbContext) : IDataSeeding
    {
        public void DataSeed()
        {
			try
			{
                if (_dbContext.Database.GetPendingMigrations().Any())
                {
                    _dbContext.Database.Migrate();
                }
                if (!_dbContext.ProductBrands.Any())
                {
                    var productbrand = File.ReadAllText("..\\Infrastructure\\Presistence\\DataSeedFiles\\brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(productbrand);
                    if (brands is not null && brands.Any())
                    {
                        _dbContext.ProductBrands.AddRange(brands);
                    }
                }

                if (!_dbContext.Products.Any())
                {
                    var products = File.ReadAllText("..\\Infrastructure\\Presistence\\DataSeedFiles\\products.json");
                    var productsdata = JsonSerializer.Deserialize<List<Product>>(products);
                    if (productsdata is not null && productsdata.Any())
                    {
                        _dbContext.Products.AddRange(productsdata);
                    }
                }

                if (!_dbContext.ProductTypes.Any())
                {
                    var productTypes = File.ReadAllText("..\\Infrastructure\\Presistence\\DataSeedFiles\\types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(productTypes);
                    if (types is not null && types.Any())
                    {
                        _dbContext.ProductTypes.AddRange(types);
                    }
                }
                _dbContext.SaveChanges();
            }
			catch (Exception)
			{

			}
        }
    }
}

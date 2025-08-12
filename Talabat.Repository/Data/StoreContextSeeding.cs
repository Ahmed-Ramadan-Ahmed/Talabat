using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data
{
    public static class StoreContextSeeding
    {
        public static async Task SeedAsync(StoreContext dbContext)
        {
            #region Brand Seeding

            if (!dbContext.ProductBrands.Any())
            {
            
                var BrandData = File.ReadAllText("../Talabat.Repository/Data/SeedData/brands.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);
                if (Brands?.Count > 0)
                {
                    foreach (var brand in Brands)
                    {
                        await dbContext.ProductBrands.AddAsync(brand);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }
            

            #endregion

            #region Type Seeding

            if(!dbContext.ProductTypes.Any())
            {
                var TypeData = File.ReadAllText("../Talabat.Repository/Data/SeedData/types.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypeData);
                if (Types?.Count > 0)
                {
                    foreach (var type in Types)
                    {
                        await dbContext.ProductTypes.AddAsync(type);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }
            
            #endregion

            #region Product Seeding

            if(!dbContext.Products.Any())
            {
                var ProductData = File.ReadAllText("../Talabat.Repository/Data/SeedData/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);
                if (Products?.Count > 0)
                {
                    foreach (var product in Products)
                    {
                        await dbContext.Products.AddAsync(product);
                    }
                }
                await dbContext.SaveChangesAsync();
            }

            #endregion
        }
    }
}
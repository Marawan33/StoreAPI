using Microsoft.Extensions.Logging;
using Store.Data.Context;
using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.Repository
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {



                if (context.ProductTypes != null && !context.ProductTypes.Any())
                {
                    var typeData = File.ReadAllText("../Store.Repository/SeedData/types.json");
                    var type = JsonSerializer.Deserialize<List<ProductType>>(typeData);
                    if (type is not null)
                    {
                        await context.ProductTypes.AddRangeAsync(type);
                    }
                }
                if (context.ProductBrands != null && !context.ProductBrands.Any())
                {
                    var brandData = File.ReadAllText("../Store.Repository/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                    if (brands is not null)
                    {
                        await context.ProductBrands.AddRangeAsync(brands);
                    }
                }
                if (context.Products != null && !context.Products.Any())
                {
                    var productdata = File.ReadAllText("../Store.Repository/SeedData/products.json");
                    var product = JsonSerializer.Deserialize<List<Product>>(productdata);
                    if (product is not null)
                    {
                        await context.Products.AddRangeAsync(product);
                    }
                }
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreDbContext>();
                logger.LogError(ex.Message);
            }
        }
    }
}
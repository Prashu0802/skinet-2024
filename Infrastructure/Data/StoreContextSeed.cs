using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedData(StoreContext context)
        {
            if (!context.Products.Any())
            {
                var productData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");
                var product=JsonSerializer.Deserialize<List<Product>>(productData);
                if (product == null) return;
                context.Products.AddRange(product);
                await context.SaveChangesAsync();
            }
        }
    }
}

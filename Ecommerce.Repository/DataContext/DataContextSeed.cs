using E_Commerce.core.Entities;
using E_Commerce.core.Entities.Order;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ecommerce.Repository.DataContext
{
    public static class DataContextSeed
    {
        public static async Task SeedDataAsync(ECommerceDataContext context)
        {
            if (!context.Set<ProductBrand>().Any())
            {
                //
                var brandsData = await File.ReadAllTextAsync(@"..\Ecommerce.Repository\DataSeeding\brands.json");

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                if (brands is not null && brands.Any())
                {
                    await context.Set<ProductBrand>().AddRangeAsync(brands);
                    await context.SaveChangesAsync();
                }
            }
            
            if (!context.Set<ProductType>().Any())
            {
                var TypeData = await File.ReadAllTextAsync(@"..\Ecommerce.Repository\DataSeeding\types.json");

                var Type = JsonSerializer.Deserialize<List<ProductType>>(TypeData);

                if (Type is not null && Type.Any())
                {
                    await context.Set<ProductType>().AddRangeAsync(Type);
                    await context.SaveChangesAsync();
                }
            }

            if (!context.Set<Product>().Any())
            {
                var ProductData = await File.ReadAllTextAsync(@"..\Ecommerce.Repository\DataSeeding\products.json");

                var product = JsonSerializer.Deserialize<List<Product>>(ProductData);

                if (product is not null && product.Any())
                {
                    await context.Set<Product>().AddRangeAsync(product);
                    await context.SaveChangesAsync();
                }
            }

            if (!context.Set<DeliveryMethod>().Any())
            {
                var deliveryData = await File
                    .ReadAllTextAsync(@"..\Ecommerce.Repository\DataSeeding\delivery.json");

                var delivery = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);

                if (delivery is not null && delivery.Any())
                {
                    await context.Set<DeliveryMethod>().AddRangeAsync(delivery);
                    await context.SaveChangesAsync();
                }
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data
{
    public static class StoreDbContextSeed
    {
        public static async Task SeedAsync(StoreDbContext dbContext)
        {
            //Seeding Brand 
            if(!dbContext.ProductBrands.Any())
            {
                var BrandsData = File.ReadAllText("../Talabat.Repository/Data/Data Seed/brands.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
                if(Brands?.Count() > 0)
                {
                    foreach(var Brand in Brands)
                    {
                        await dbContext.Set<ProductBrand>().AddAsync(Brand);
                    }
                   await dbContext.SaveChangesAsync();
                }
            }
            // Seeding Product Type
            if (!dbContext.ProductTypes.Any())
            {
                var TypessData = File.ReadAllText("../Talabat.Repository/Data/Data Seed/types.json");
                var Typess = JsonSerializer.Deserialize<List<ProductType>>(TypessData);
                if (Typess?.Count() > 0)
                {
                    foreach (var type in Typess)
                    {
                        await dbContext.Set<ProductType>().AddAsync(type);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }

            // Seeding Product
            if (!dbContext.Products.Any())
            {
                var ProductsData = File.ReadAllText("../Talabat.Repository/Data/Data Seed/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                if (products?.Count() > 0)
                {
                    foreach (var product in products)
                    {
                        await dbContext.Set<Product>().AddAsync(product);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }

            //seeding Delivery Methods
            if (!dbContext.DeliveryMethods.Any())
            {
                var DeliveryMethodsData = File.ReadAllText("../Talabat.Repository/Data/Data Seed/delivery.json");
                var DeliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodsData);
                if(DeliveryMethods?.Count() > 0)
                {
                    foreach(var DeliverMethod in DeliveryMethods)
                           await dbContext.Set<DeliveryMethod>().AddAsync(DeliverMethod);
                }
                await dbContext.SaveChangesAsync();
            }
        }
    }
}

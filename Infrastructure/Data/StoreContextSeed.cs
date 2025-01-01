using Core.Entities;
using Core.Entities.Identity;
using Core.Entities.OrderAgregates;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreDbContext storeDbContext, ILoggerFactory loggerFactory)
        {
            try {

                if (!storeDbContext.Brands.AsNoTracking().Any()) {
                    var brandData =  await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/brands.json");

                    var brands = JsonSerializer.Deserialize<List<Brand>>(brandData);

                    await storeDbContext.Brands.AddRangeAsync(brands);

                    await storeDbContext.SaveChangesAsync();
                }

                if (!storeDbContext.Categories.AsNoTracking().Any())
                {
                    var categoriesData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/categories.json");

                    var categories = JsonSerializer.Deserialize<List<Category>>(categoriesData);

                    await storeDbContext.Categories.AddRangeAsync(categories);

                    await storeDbContext.SaveChangesAsync();
                }

                if (!storeDbContext.Products.AsNoTracking().Any())
                {
                    var productsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");

                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    
                    await storeDbContext.Products.AddRangeAsync(products);

                    await storeDbContext.SaveChangesAsync();
                }

                if (!storeDbContext.DeliveryMethods.AsNoTracking().Any())
                {
                    var deliveryMethodsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/deliveryMethods.json");

                    var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsData);

                    await storeDbContext.DeliveryMethods.AddRangeAsync(deliveryMethods);

                    await storeDbContext.SaveChangesAsync();
                }

            }
            catch (Exception ex) {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex, "Error in Seeding Data: {ErrorMessage}", ex.Message);
            }
        }

        public static async Task SeedUserAsync(UserManager<ApplicationUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new ApplicationUser()
                {
                    UserName = "Aya_Nassar",
                    DisplayName = "Aya Nassar",
                    Email = "Aya.m.nasar@gmail.com",
                    Address = new Address()
                    {
                        Country = "Egypt",
                        Street = "10 Street",
                        City = "Sers Elyanh",
                        State = "Menofia",
                        ZipCode = "32861"
                    }
                };

                await userManager.CreateAsync(user, "Af36136026$");
            }
        }
    }
}

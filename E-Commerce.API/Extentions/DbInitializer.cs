using E_Commerce.core.Entities.Identity;
using Ecommerce.Repository.DataContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.API.Extentions
{
    public static class DbInitializer
    {
        public static async Task InitializeDbAsync(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                var loggerFactory = service.GetRequiredService<ILoggerFactory>();

                try
                {
                    var context = service.GetRequiredService<ECommerceDataContext>();
                    var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();
                    if ((await context.Database.GetPendingMigrationsAsync()).Any())
                    {
                        await context.Database.MigrateAsync();
                    }
                    await DataContextSeed.SeedDataAsync(context);
                    await IdentityDataContextSeed.SeedUserAsync(userManager);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex.Message);

                }


            }
        }

    }
}

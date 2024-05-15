using E_Commerce.API.Errors;
using E_Commerce.API.Extentions;
using E_Commerce.core.Interfaces.Repositories;
using E_Commerce.core.Interfaces.Services;
using E_Commerce.Services;
using Ecommerce.Repository.DataContext;
using Ecommerce.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Reflection;

namespace E_Commerce.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ECommerceDataContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConection"));
            });
            builder.Services.AddDbContext<IdentityDataContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentitySqlConection"));
            });

            builder.Services.AddAplicationServices(builder.Configuration);
            builder.Services.AddIdentityServices(builder.Configuration);

            var app = builder.Build();
            await DbInitializer.InitializeDbAsync(app);
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();

            app.MapControllers();
            app.UseMiddleware<CustomExceptionHandeller>();
            app.Run();
        }

    }
}

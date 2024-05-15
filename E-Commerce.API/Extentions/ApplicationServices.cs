using E_Commerce.API.Errors;
using E_Commerce.core.Interfaces.Repositories;
using E_Commerce.core.Interfaces.Services;
using E_Commerce.Services;
using Ecommerce.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Reflection;


namespace E_Commerce.API.Extentions
{
    public static class ApplicationServices
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped<IProductService, ProductServices>();
            services.AddScoped<IOrderService, OrderServices>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IUserService , UserServices>();
            services.AddScoped<ITokenService ,TokenServices>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBasketServices, BasketServices>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ICashService, CashServices>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddSingleton<IConnectionMultiplexer>(opt =>
            {
                var config = ConfigurationOptions.Parse(Configuration.GetConnectionString("RedisConnection"));
                return ConnectionMultiplexer.Connect(config);
            });
            services.AddControllers();
            services.AddSwaggerServices();
            services.Configure<ApiBehaviorOptions>(option =>
            {
                option.InvalidModelStateResponseFactory = context =>
                {
                    var Errors = context.ModelState.Where(m => m.Value.Errors.Any()).SelectMany(m => m.Value.Errors)
                    .Select(e => e.ErrorMessage).ToList();
                    return new BadRequestObjectResult(new ApiValidationErrorResponse()
                    {
                        Errors = Errors
                    });
                };
            });
            return services;
        }
    }
}

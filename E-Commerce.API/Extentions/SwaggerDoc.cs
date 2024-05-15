using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace E_Commerce.API.Extentions
{
    public static class SwaggerDoc
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            //services.AddSwaggerGen(option =>
            //{
            //    var scheme = new OpenApiSecurityScheme() { 
            //    Description ="Standerd Authorization header using the bearer schema, e.g.\"bearer {token}\"",
            //    In = ParameterLocation.Header,
            //    Name= "Authorization",
            //    Type = SecuritySchemeType.ApiKey
            //    };
            //    option.AddSecurityDefinition("bearer" , scheme);
            //    option.OperationFilter<SecurityRequirementsOperationFilter>();
            ////}); 

            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "ECommerce App", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
            return services;
        }
    }
}

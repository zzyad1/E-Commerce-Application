using System.Text.Json;
using System.Net;

namespace E_Commerce.API.Errors
{
    public class CustomExceptionHandeller
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandeller> _logger;
        private readonly IHostEnvironment _environment;
        public CustomExceptionHandeller(RequestDelegate next, ILogger<CustomExceptionHandeller> logger, IHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var response = _environment.IsDevelopment() ? new ApiExceptionResponse(500 , ex.Message , ex.StackTrace) 
                    : new ApiExceptionResponse(500);
                var json = JsonSerializer.Serialize(response , new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }); 
                await context.Response.WriteAsync(json);

                //await context.Response.WriteAsJsonAsync(response);

            }
        }
    }
}

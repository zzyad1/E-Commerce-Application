using E_Commerce.core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace E_Commerce.API.Helper
{
    public class CashAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeout;

        public CashAttribute(int timeout)
        {
            _timeout = timeout;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cashKey = GenerateKeyFromRequest(context.HttpContext.Request);
            var _cashService = context.HttpContext.RequestServices.GetRequiredService<ICashService>();

            var cashResponse = await _cashService.GetCashResponseAsync(cashKey);
            if (cashResponse is not null)
            {
                var result = new ContentResult()
                {
                    ContentType = "application/json",
                    StatusCode = 200,
                    Content = cashResponse
                };
                context.Result = result;
                return;
            }
            var executedContext = await next();
            if (executedContext.Result is OkObjectResult response)
                await _cashService.SetCashResponseAsync(cashKey, response.Value, TimeSpan.FromSeconds( _timeout));

        }

        private string GenerateKeyFromRequest(HttpRequest httpRequest)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{httpRequest.Path}");
            foreach (var item in httpRequest.Query.OrderBy(z=>z.Key))
            {
                sb.Append($"{item}");
            }
            return sb.ToString();
        }
    }
}

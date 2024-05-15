using E_Commerce.core.DataTransferObjects;
using E_Commerce.core.Interfaces.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IConfiguration _configuration;
        public PaymentController(IPaymentService paymentService, IConfiguration configuration)
        {
            _paymentService = paymentService;
            _configuration = configuration;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<BasketDto>> CreatePaymentIntent(BasketDto basketDto)
        {
            var Basket = await _paymentService.CreateOrUpdatePaymentIntentForExistingOrder(basketDto);
            return Ok(Basket);
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], _configuration["Stripe:endpointSecret"]);

                // Handle the event
                if (stripeEvent.Type == Events.PaymentIntentPaymentFailed)
                {
                    var PaymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    await _paymentService.UpdatePaymentStatusFieled(PaymentIntent.Id);
                }
                else if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    var PaymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    await _paymentService.UpdatePaymentStatusSuccessed(PaymentIntent.Id);
                }
                // ... handle other event types
                else
                {
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }
    }
}

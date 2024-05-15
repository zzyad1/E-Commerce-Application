using AutoMapper;
using E_Commerce.core.DataTransferObjects;
using Product = E_Commerce.core.Entities.Product;
using E_Commerce.core.Interfaces.Repositories;
using E_Commerce.core.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Stripe;
using E_Commerce.core.Entities.Order;
using Ecommerce.Repository.Spcifications;

namespace E_Commerce.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketServices _basketServices;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public PaymentService(IUnitOfWork unitOfWork, IBasketServices basketServices, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _basketServices = basketServices;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<BasketDto> CreateOrUpdatePaymentIntentForExistingOrder(BasketDto basketDto)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            foreach (var item in basketDto.BasketItems)
            {
                var product =await _unitOfWork.Repository<Product ,int>().GetAsync(item.ProductId);
                if (product?.Price != item.Price)
                    item.Price = product.Price;
            }
            var total = basketDto.BasketItems.Sum(b => b.Price * b.Quntity);

            if (!basketDto.DeliveryMethodId.HasValue) throw new Exception("No Delivery Method select");
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetAsync(basketDto.DeliveryMethodId.Value);
            var ShippingPrice = deliveryMethod.Price;

            long amount = (long)((total * 100) + (ShippingPrice * 100));

            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;
            if(string.IsNullOrWhiteSpace(basketDto.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount =amount,
                    Currency = "USD",
                    PaymentMethodTypes = new List<string> { "card"} 
                };
                paymentIntent = await service.CreateAsync(options);
                basketDto.PaymentIntentId = paymentIntent.Id;
                basketDto.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = amount
                };
                await service.UpdateAsync(basketDto.PaymentIntentId, options);
            }
            await _basketServices.UpdateBasketAsync(basketDto);
            return basketDto;
        }

        public async Task<BasketDto> CreateOrUpdatePaymentIntentForNewOrder(string BasketId)
        {

            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            var basketDto = await _basketServices.GetBasketAsync(BasketId);

            foreach (var item in basketDto.BasketItems)
            {
                var product = await _unitOfWork.Repository<Product, int>().GetAsync(item.ProductId);
                if (product?.Price != item.Price)
                    item.Price = product.Price;
            }
            var total = basketDto.BasketItems.Sum(b => b.Price * b.Quntity);

            if (!basketDto.DeliveryMethodId.HasValue) throw new Exception("No Delivery Method select");
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetAsync(basketDto.DeliveryMethodId.Value);
            var ShippingPrice = deliveryMethod.Price;

            long amount = (long)((total * 100) + (ShippingPrice * 100));

            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;
            if (string.IsNullOrWhiteSpace(basketDto.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = amount,
                    Currency = "USD",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                paymentIntent = await service.CreateAsync(options);
                basketDto.PaymentIntentId = paymentIntent.Id;
                basketDto.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = amount
                };
                await service.UpdateAsync(basketDto.PaymentIntentId, options);
            }
            await _basketServices.UpdateBasketAsync(basketDto);
            return basketDto;
        }

        public async Task<OrderResultDto> UpdatePaymentStatusFieled(string PaymentId)
        {
            var spec = new OrderWithPaymentIntentSpcification(PaymentId);
            var order = await _unitOfWork.Repository<Order, Guid>().GetWithSpecAsync(spec);
            if (order == null) throw new Exception($"No Order With PaymentIntentId {PaymentId}");

            order.PaymentStatus = PaymentStatus.Failed;
            _unitOfWork.Repository<Order,Guid>().Update(order);

            await _unitOfWork.CompleteAsync();
            return _mapper.Map<OrderResultDto>(order);

        }

        public async Task<OrderResultDto> UpdatePaymentStatusSuccessed(string PaymentId)
        {
            throw new NotImplementedException(); var spec = new OrderWithPaymentIntentSpcification(PaymentId);
            var order = await _unitOfWork.Repository<Order, Guid>().GetWithSpecAsync(spec);
            if (order == null) throw new Exception($"No Order With PaymentIntentId {PaymentId}");

            order.PaymentStatus = PaymentStatus.Received;
            _unitOfWork.Repository<Order, Guid>().Update(order);

            await _unitOfWork.CompleteAsync();

            await _basketServices.DeleteBasketAsync(order.BasketId);
            return _mapper.Map<OrderResultDto>(order);
        }
    }
}

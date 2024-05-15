using AutoMapper;
using E_Commerce.core.DataTransferObjects;
using E_Commerce.core.Entities;
using E_Commerce.core.Entities.Order;
using E_Commerce.core.Interfaces.Repositories;
using E_Commerce.core.Interfaces.Services;
using Ecommerce.Repository.Spcifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class OrderServices : IOrderService
    {
        private readonly IBasketServices _basketServices;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymentService;
        public OrderServices(IBasketServices basketServices, IUnitOfWork unitOfWork, IMapper mapper, IPaymentService paymentService)
        {
            _basketServices = basketServices;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _paymentService = paymentService;
        }

        public async Task<OrderResultDto> CreateOrdersAsync(OrderDto orderDto)
        {
            var basket = await _basketServices.GetBasketAsync(orderDto.BasketId);
            if (basket == null) throw new Exception($"no basket with this id {orderDto.BasketId} was found");

            var orderItems = new List<OrderItemDto>();
            foreach (var BasketItem in basket.BasketItems)
            {
                var product =await _unitOfWork.Repository<Product ,int>().GetAsync(BasketItem.ProductId);
                if (product == null) continue;

                var productItem = new OrderItemProduct
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    ProductUrl = product.PictureUrl
                };

                var orderItem = new OrderItem
                {
                    OrderItemProduct = productItem,
                    Price = product.Price,
                    Quantity = BasketItem.Quntity
                };
                var mappedItem = _mapper.Map<OrderItemDto>(orderItem);
                orderItems.Add(mappedItem);
            }

            if (!orderItems.Any()) throw new Exception("no basket item found");

            if (!orderDto.DeliveryMethodId.HasValue) throw new Exception("no Delivery method was selected");
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod ,int>().GetAsync(orderDto.DeliveryMethodId.Value);
            if (deliveryMethod == null) throw new Exception("Invalid Delivery method id");

            var shippingAddress =_mapper.Map<ShippingAddress>(orderDto.ShippingAddress);

            var spec = new OrderWithPaymentIntentSpcification(basket.PaymentIntentId);
            var existingOrder = await _unitOfWork.Repository<Order, Guid>().GetWithSpecAsync(spec);
            if (existingOrder != null)
            {
                _unitOfWork.Repository<Order , Guid>().Delete(existingOrder);
                await _paymentService.CreateOrUpdatePaymentIntentForExistingOrder(basket);
            }
            else
            {
                basket = await _paymentService.CreateOrUpdatePaymentIntentForExistingOrder(basket);
            }

            var subTotal =orderItems.Sum(i=>i.Price *  i.Quantity);

            var mappedItems = _mapper.Map<List<OrderItem>>(orderItems);

            var order = new Order
            {
                BuyerEmail = orderDto.BuyerEmail,
                ShippingAddress = shippingAddress,
                DeliveryMethod = deliveryMethod,
                OrderItems = mappedItems,
                SubTotal = subTotal,
                PaymentIntentId = basket.PaymentIntentId,
                BasketId = basket.Id,
            };
            await _unitOfWork.Repository<Order ,Guid>().AddAsync(order);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<OrderResultDto>(order);
        }

        public async Task<IEnumerable<OrderResultDto>> GetAllOrdersAsync(string Email)
        {
            var spec = new OrderSpcification(Email);
            var order = await _unitOfWork.Repository<Order, Guid>().GetAllWithSpecAsync(spec);
            if (!order.Any()) throw new Exception($"No Orders Found For {Email}");
            return _mapper.Map<IEnumerable<OrderResultDto>>(order);
        }
        public async Task<OrderResultDto> GetOrdersAsync(Guid id, string Email)
        {
            var spec = new OrderSpcification(id ,Email);
            var order = await _unitOfWork.Repository<Order, Guid>().GetWithSpecAsync(spec);
            if (order is null) throw new Exception($"No Orders Found For ID {id}");
            return _mapper.Map<OrderResultDto>(order);
        }

        public async Task<IEnumerable<DeliveryMethod>> GetDeliveryMethodsAsync()
        => await _unitOfWork.Repository<DeliveryMethod, int>().GetAllAsync();

    }
}

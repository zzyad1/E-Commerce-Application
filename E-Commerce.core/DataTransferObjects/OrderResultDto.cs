using E_Commerce.core.Entities.Order;

namespace E_Commerce.core.DataTransferObjects
{
    public class OrderResultDto 
    {
        public Guid Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public AddressDto ShippingAddress { get; set; }
        public string DeliveryMethod { get; set; }
        public IEnumerable<OrderItemDto> OrderItems { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public decimal SubTotal { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? BasketId { get; set; }
        public decimal ShippingPrice { get; set; }
        public decimal Total { get; set; }

    }
}

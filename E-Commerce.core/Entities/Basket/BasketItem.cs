namespace E_Commerce.core.Entities.Basket
{
    public class BasketItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quntity { get; set;}
        public string PictureUrl { get; set; }
        public string TypeName { get; set; }
        public string BrandName { get; set; }


    }
}
using E_Commerce.core.Entities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Repository.DataContext.Configration
{
    public class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(o=>o.OrderItemProduct,z=>z.WithOwner());
            builder.Property(o=>o.Price).HasColumnType("decimal(18,5)");
        }
    }

}

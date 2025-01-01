using Core.Entities.OrderAgregates;
using Core.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(order => order.ShipToAddress, a =>
            {
                a.WithOwner();
            });

            builder.Property(order => order.Status)
                .HasConversion(
                status => status.ToString(),
                status => (OrderStatus) Enum.Parse(typeof(OrderStatus), status)
                );

            builder.HasMany(order => order.Items)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(order => order.SubTotal)
                .HasPrecision(18, 2);
        }
    }
}

using Core.Entities.OrderAgregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(orderItem => orderItem.OrderedItem, orderedItem => orderedItem.WithOwner());

            builder.Property(orderItem => orderItem.Price)
                   .HasPrecision(18, 2);

        }
    }
}

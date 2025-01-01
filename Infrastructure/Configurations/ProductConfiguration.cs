using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Price)
                .HasPrecision(18, 2);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Description)
                .HasMaxLength(300);

            builder.HasOne(b => b.Brand).WithMany()
                .HasForeignKey(b => b.BrandId);

            builder.HasOne(b => b.Category).WithMany()
                .HasForeignKey(b => b.CategoryId);

            builder.ToTable("Products"); // Optional: Specify table name
        }
    }
}

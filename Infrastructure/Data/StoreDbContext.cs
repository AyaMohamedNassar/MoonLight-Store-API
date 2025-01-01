using Core.Entities;
using Core.Entities.Identity;
using Core.Entities.OrderAgregates;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data
{
    public class StoreDbContext : IdentityDbContext<ApplicationUser>
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
            
        }

        public DbSet<Product> Products { set; get; }
        public DbSet<Brand> Brands { set; get; }
        public DbSet<Category> Categories { set; get; }
        public DbSet<ApplicationUser> Users { set; get; }
        public DbSet<Address> Addresses { set; get; }
        public DbSet<Order> Orders { set; get; }
        public DbSet<OrderItem> OrderItems {  set; get; }
        public DbSet<DeliveryMethod> DeliveryMethods {  set; get; }
        public DbSet<EmailConfirmationToken> EmailConfirmationTokens { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }


    }
}

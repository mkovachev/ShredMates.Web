using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShredMates.Data.Models;

namespace ShredMates.Data
{
    public class ShredMatesDbContext : IdentityDbContext<User>
    {
        public ShredMatesDbContext(DbContextOptions<ShredMatesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // connections
            builder.Entity<Product>().ToTable("Products");
            builder.Entity<Category>().ToTable("Categories");
            builder.Entity<Order>().ToTable("Orders");
            builder.Entity<OrderDetail>().ToTable("OrderDetails");

            base.OnModelCreating(builder);
        }
    }
}
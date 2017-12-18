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

        // db sets
        public DbSet<AllProductServiceModel> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        //public DbSet<ShoppingCart> ShoppingCart { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // connections
            builder.Entity<AllProductServiceModel>().ToTable("Products");
            builder.Entity<Category>().ToTable("Categories");
            //builder.Entity<ShoppingCart>().ToTable("ShoppingCart");
            builder.Entity<ShoppingCartItem>().ToTable("ShoppingCartItems");
            builder.Entity<Order>().ToTable("Orders");
            builder.Entity<OrderDetail>().ToTable("OrderDetails");

            base.OnModelCreating(builder);
        }
    }
}

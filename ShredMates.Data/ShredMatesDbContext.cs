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
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            // connections
            builder.Entity<Product>().ToTable("Product");
            builder.Entity<Category>().ToTable("Category");

            base.OnModelCreating(builder);
        }
    }
}

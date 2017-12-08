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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
     
        }
    }
}

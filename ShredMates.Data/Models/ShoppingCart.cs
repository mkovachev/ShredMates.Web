using System.Collections.Generic;

namespace ShredMates.Data.Models
{
    public class ShoppingCart
    {
        private readonly ShredMatesDbContext db;

        public ShoppingCart(ShredMatesDbContext db)
        {
            this.db = db;
        }

        public string Id { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}

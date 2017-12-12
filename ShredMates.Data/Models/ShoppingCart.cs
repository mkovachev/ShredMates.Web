using System.Collections.Generic;

namespace ShredMates.Data.Models
{
    public class ShoppingCart
    {
        public string Id { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}

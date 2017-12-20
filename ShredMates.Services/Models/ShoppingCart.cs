using System.Collections.Generic;

namespace ShredMates.Services.Models
{
    public class ShoppingCart
    {
        public string Id { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}

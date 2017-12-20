using ShredMates.Data.Models;

namespace ShredMates.Services.Models
{
    public class ShoppingCartItem
    {
        public string Id { get; set; }

        public Product Product { get; set; }

        public int Amount { get; set; }

        public string ShoppingCartId { get; set; } 
    }
}

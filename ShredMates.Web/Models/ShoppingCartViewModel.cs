using ShredMates.Data.Models;

namespace ShredMates.Web.Models
{
    public class ShoppingCartViewModel
    {
        public ShoppingCart ShoppingCart { get; set; }

        public decimal ShoppingCartTotal { get; set; }
    }
}

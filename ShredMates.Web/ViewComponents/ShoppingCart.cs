using Microsoft.AspNetCore.Mvc;
using ShredMates.Services.Interfaces;
using ShredMates.Web.Models;
using System.Threading.Tasks;

namespace ShredMates.Web.ViewComponents
{
    public class ShoppingCart : ViewComponent
    {
        private readonly ShredMates.Services.Models.ShoppingCart shoppingCart;
        private readonly IShoppingCartService shoppingCartServices;

        public ShoppingCart(ShredMates.Services.Models.ShoppingCart shoppingCart, IShoppingCartService shoppingCartServices)
        {
            this.shoppingCart = shoppingCart;
            this.shoppingCartServices = shoppingCartServices;
        }

        // same as ShoppingCartController Index
        public async Task<IViewComponentResult> InvokeAsync()
            => await Task.Run(() => View(new ShoppingCartViewModel
            {
                ShoppingCart = shoppingCart,
                ShoppingCartTotal = this.shoppingCartServices.GetTotal()
            }));
    }
}

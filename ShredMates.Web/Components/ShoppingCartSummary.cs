using Microsoft.AspNetCore.Mvc;
using ShredMates.Data.Models;
using ShredMates.Services.Interfaces;
using ShredMates.Web.Models;
using System.Threading.Tasks;

namespace ShredMates.Web.Components
{
    public class ShoppingCartSummary: ViewComponent
    {
        private readonly ShoppingCart shoppingCart;
        private readonly IShoppingCartService shoppingCartServices;

        public ShoppingCartSummary(ShoppingCart shoppingCart, IShoppingCartService shoppingCartServices)
        {
            this.shoppingCart = shoppingCart;
            this.shoppingCartServices = shoppingCartServices;
        }

        // same as ShoppingCartController Index
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await this.shoppingCartServices.AllProductsAsync();
            this.shoppingCart.ShoppingCartItems = items;

            return View(new ShoppingCartViewModel
            {
                ShoppingCart = shoppingCart,
                ShoppingCartTotal = await this.shoppingCartServices.GetTotalAsync()
            });
        }
    }
}

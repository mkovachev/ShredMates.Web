using Microsoft.AspNetCore.Mvc;
using ShredMates.Services.Interfaces;
using ShredMates.Services.Models;
using ShredMates.Web.Models;
using System.Threading.Tasks;

namespace ShredMates.Web.ViewComponents
{
    public class ShoppingCartSummary : ViewComponent
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
            => await Task.Run(() => View(new ShoppingCartViewModel
            {
                ShoppingCart = shoppingCart,
                ShoppingCartTotal = this.shoppingCartServices.GetTotal()
            }));
    }
}

using Microsoft.AspNetCore.Mvc;
using ShredMates.Data.Models;
using ShredMates.Services.Interfaces;
using System.Threading.Tasks;

namespace ShredMates.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IShoppingCartService shoppingCartServices;
        private readonly ShoppingCart shoppingCart;

        public OrderController(IShoppingCartService shoppingCartServices, ShoppingCart shoppingCart)
        {
            this.shoppingCartServices = shoppingCartServices;
            this.shoppingCart = shoppingCart;
        }

        public async Task<IActionResult> Checkout(Order order)
        {
            var items = await this.shoppingCartServices.AllItemsAsync();
            this.shoppingCart.ShoppingCartItems = items;

            if (shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Your card is empty, add some products first");
            }

            if (!ModelState.IsValid)
            {
                return View(order);
            }

            await shoppingCartServices.CreateOrderAsync(order);
            await shoppingCartServices.ClearCartAsync();

            return RedirectToAction("/");

        }
    }
}
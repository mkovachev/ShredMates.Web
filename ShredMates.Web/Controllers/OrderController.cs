using Microsoft.AspNetCore.Mvc;
using ShredMates.Data.Models;
using ShredMates.Services.Interfaces;
using ShredMates.Web.Infrastructure.Extensions;
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

        [HttpPost]
        public async Task<IActionResult> Checkout(Order order)
        {
            var items = await this.shoppingCartServices.AllProductssAsync();
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

            TempData.AddSuccessMessage("Redirecting to checkout...");
            return RedirectToAction("/");
        }

        public async Task<IActionResult> CheckoutComplete()
        {
            TempData.AddSuccessMessage("Thank you for your order! Check your email for the order details");
            return await Task.Run(() => View());
        }
    }
}
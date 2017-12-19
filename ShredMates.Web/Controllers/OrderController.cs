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

        public async Task<IActionResult> Checkout(Order order)
        {
            var items = await this.shoppingCartServices.AllProductsAsync();
            this.shoppingCart.ShoppingCartItems = items;

            if (shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("Empty cart", "Your cart is empty, go grab some products");
                TempData.AddErrorMessage("Your cart is empty, go back and add some products");
            }

            if (!ModelState.IsValid)
            {
                //TempData.AddErrorMessage("Please fill in all fields");
                return View(order);
            }

            await shoppingCartServices.CreateOrderAsync(order);
            await shoppingCartServices.ClearCartAsync();

            TempData.AddSuccessMessage("Thank you for your order! Check your email for the order details");
            return RedirectToAction("Index", "Home");
        }
    }
}
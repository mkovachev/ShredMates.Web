using Microsoft.AspNetCore.Mvc;
using ShredMates.Data.Models;
using ShredMates.Services.Interfaces;
using ShredMates.Services.Models;
using ShredMates.Web.Infrastructure.Extensions;
using System.Threading.Tasks;

namespace ShredMates.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IShoppingCartService shoppingCartServices;
        private readonly ShoppingCart shoppingCart;
        private readonly IOrderService orderService;

        public OrderController(IShoppingCartService shoppingCartServices, ShoppingCart shoppingCart, IOrderService orderService)
        {
            this.shoppingCartServices = shoppingCartServices;
            this.shoppingCart = shoppingCart;
            this.orderService = orderService;
        }

        public async Task<IActionResult> Checkout(Order order)
        {
            var items = this.shoppingCartServices.AllProducts();
            this.shoppingCart.ShoppingCartItems = items;

            if (shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("Empty cart", "Your cart is empty, go grab some products");
                TempData.AddErrorMessage("Your cart is empty, go back and add some products");
            }

            if (!ModelState.IsValid)
            {
                // TempData.AddErrorMessage("Please fill in all fields");
                return View(order);
            }

            await orderService.CreateOrderAsync(order);
            shoppingCartServices.ClearCart();

            // send email with order details to customer

            TempData.AddSuccessMessage("Thank you for your order! Check your email for the order details");
            return RedirectToAction("Index", "Home");
        }
    }
}
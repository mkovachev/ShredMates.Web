using Microsoft.AspNetCore.Mvc;
using ShredMates.Data.Models;
using ShredMates.Services.Interfaces;
using ShredMates.Web.Infrastructure.Extensions;
using ShredMates.Web.Models;
using System.Threading.Tasks;

namespace ShredMates.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService shoppingCartServices;
        private readonly ShoppingCart shoppingCart;

        public ShoppingCartController(IShoppingCartService shoppingCartServices, ShoppingCart shoppingCart)
        {
            this.shoppingCartServices = shoppingCartServices;
            this.shoppingCart = shoppingCart;
        }

        public async Task<IActionResult> Index()
        {
            var items = await this.shoppingCartServices.AllProductssAsync();
            this.shoppingCart.ShoppingCartItems = items;

            return View(new ShoppingCartViewModel
            {
                ShoppingCart = shoppingCart,
                ShoppingCartTotal = await this.shoppingCartServices.GetTotalAsync()
            });
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            var shoppingCartItem = await this.shoppingCartServices.FindProductByIdAsync(id);

            if (shoppingCartItem == null)
            {
                return NotFound();
            }

            await this.shoppingCartServices.AddToCartAsync(shoppingCartItem, 1);

            TempData.AddSuccessMessage($"{shoppingCartItem.Title} successfully added to cart");

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var shoppingCartItem = await this.shoppingCartServices.FindProductByIdAsync(id);

            if (shoppingCartItem == null)
            {
                return NotFound();
            }

            await this.shoppingCartServices.RemoveProductAsync(shoppingCartItem);

            TempData.AddSuccessMessage($"{shoppingCartItem.Title} successfully removed from cart");

            return RedirectToAction("Index", nameof(shoppingCart));
        }
    }
}
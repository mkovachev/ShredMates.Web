using Microsoft.AspNetCore.Mvc;
using ShredMates.Data.Models;
using ShredMates.Services.Interfaces;
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
            var items = await this.shoppingCartServices.AllItemsAsync();
            this.shoppingCart.ShoppingCartItems = items;

            return View(new ShoppingCartViewModel
            {
                ShoppingCart = shoppingCart,
                ShoppingCartTotal = await this.shoppingCartServices.GetTotalAsync()
            });
        }

        public async Task<IActionResult> AddToCart(int productId)
        {
            var shoppingCartItem = await this.shoppingCartServices.FindItemByIdAsync(productId);

            if (shoppingCartItem == null)
            {
                return NotFound();
            }

            await this.shoppingCartServices.AddItemAsync(shoppingCartItem.Product, 1);

            return RedirectToAction("/");
        }

        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var shoppingCartItem = await this.shoppingCartServices.FindItemByIdAsync(productId);

            if (shoppingCartItem == null)
            {
                return NotFound();
            }

            await this.shoppingCartServices.RemoveItemAsync(shoppingCartItem.Product);

            return RedirectToAction("Index");
        }
    }
}
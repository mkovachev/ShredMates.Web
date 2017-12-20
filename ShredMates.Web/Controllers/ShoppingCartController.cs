using Microsoft.AspNetCore.Mvc;
using ShredMates.Services.Interfaces;
using ShredMates.Services.Models;
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
            => await Task.Run(() => View(new ShoppingCartViewModel
            {
                ShoppingCart = shoppingCart,
                ShoppingCartTotal = this.shoppingCartServices.GetTotal()
            }));

        public async Task<IActionResult> AddToCart(int id)
        {
            var shoppingCartItem = await this.shoppingCartServices.FindProductByIdAsync(id);

            if (shoppingCartItem == null)
            {
                return NotFound();
            }

            this.shoppingCartServices.AddToCart(shoppingCartItem, 1);

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

            this.shoppingCartServices.RemoveProduct(shoppingCartItem);

            TempData.AddSuccessMessage($"{shoppingCartItem.Title} amount updated successfully");
            //this.AddToastMessage("Toastr", "Remove ${shoppingCartItem.Title}", ToastType.Success);

            return RedirectToAction("Index", nameof(shoppingCart));
        }
    }
}
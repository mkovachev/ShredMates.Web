using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShredMates.Data;
using ShredMates.Services.Interfaces;
using ShredMates.Services.Models;
using ShredMates.Web.Models;
using ShredMates.Web.Models.HomeViewModels;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ShredMates.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService products;
        private readonly ShoppingCart shoppingCart;

        private const string sessionKey = "session";

        public HomeController(IProductService products, ShoppingCart shoppingCart)
        {
            this.products = products;
            this.shoppingCart = shoppingCart;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var sessionId = this.HttpContext.Session.GetString(sessionKey);

            if (sessionId == null)
            {
                sessionId = this.shoppingCart.Id;
                this.HttpContext.Session.SetString(sessionKey, sessionId);
            }

            return View(new HomeViewModel
            {
                Products = await this.products.AllAsync(page, DataConstants.PageSize),
                Current = page,
                TotalPages = (int)Math.Ceiling(this.products.TotalPages() / (double)DataConstants.PageSize)
            });
        }

        public async Task<IActionResult> Search(HomeViewModel model)
           => View(new SearchViewModel
           {
               Search = model.Search,
               Products = await this.products.SearchAsync(model.Search)
           });

        public async Task<IActionResult> About() => await Task.Run(() => View());

        public async Task<IActionResult> Contact() => await Task.Run(() => View());

        public async Task<IActionResult> Error()
             => await Task.Run(() =>
                    View(new ErrorViewModel
                    {
                        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                    }));
    }
}

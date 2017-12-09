using Microsoft.AspNetCore.Mvc;
using ShredMates.Services.Interfaces;
using ShredMates.Web.Models;
using ShredMates.Web.Models.HomeViewModels;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ShredMates.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService products;

        public HomeController(IProductService products)
        {
            this.products = products;
            // this.HttpContext.Session; // shopping cart
        }

        public async Task<IActionResult> Index()
            => View(new HomeIndexViewModel
            {
                Products = await this.products.AllAsync()
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

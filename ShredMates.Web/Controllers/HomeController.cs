using Microsoft.AspNetCore.Mvc;
using ShredMates.Services.Interfaces;
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

        private const int PageSize = 12;

        public HomeController(IProductService products)
        {
            this.products = products;
        }

        public async Task<IActionResult> Index(int page = 1)
            => View(new HomeViewModel
            {
                Products = await this.products.AllAsync(page, PageSize),
                Current = page,
                TotalPages = (int)Math.Ceiling(this.products.TotalPages() / (double)PageSize)
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
=======
using ShredMates.Services.Admin.Interfaces;
using ShredMates.Web.Models;
>>>>>>> master

namespace ShredMates.Web.Controllers
{
    public class ProductsController : Controller
    {
<<<<<<< HEAD
        public IActionResult Details()
        {
            return View();
=======
        private readonly IAdminProductService products;

        public ProductsController(IAdminProductService products)
        {
            this.products = products;
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await this.products.FindByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(new ProductDetailsViewModel
            {
                Title = product.Title,
                Description = product.Description,
                ImageUrl = product.ImageUrl
            });
>>>>>>> master
        }
    }
}
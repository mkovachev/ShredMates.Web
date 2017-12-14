using Microsoft.AspNetCore.Mvc;
using ShredMates.Data;
using ShredMates.Services.Interfaces;
using ShredMates.Web.Models;
using System;
using System.Threading.Tasks;

namespace ShredMates.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService categories;

        public CategoriesController(ICategoryService categories)
        {
            this.categories = categories;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            return View(new CategoryViewModel
            {
                Products = await this.categories.AllProductsInCategoryAsync(page, DataConstants.PageSize),
                Current = page,
                TotalPages = (int)Math.Ceiling(this.categories.TotalPages() / (double)DataConstants.PageSize)
            });
        }


    }
}
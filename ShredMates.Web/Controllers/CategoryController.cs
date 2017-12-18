using Microsoft.AspNetCore.Mvc;
using ShredMates.Data;
using ShredMates.Services.Interfaces;
using ShredMates.Web.Models;
using System;
using System.Threading.Tasks;

namespace ShredMates.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categories;

        public CategoryController(ICategoryService categories)
        {
            this.categories = categories;
        }

        public async Task<IActionResult> Index(int id, int page = 1)
        {
            if (!int.TryParse(id.ToString(), out var n))
            {
                return NotFound();
            }

            return View(new CategoryViewModel
            {
                Products = await this.categories.AllProductsInCategoryAsync(id, page, DataConstants.PageSize),
                Current = page,
                TotalPages = (int)Math.Ceiling(this.categories.TotalPages() / (double)DataConstants.PageSize)
            });
        }

    }
}
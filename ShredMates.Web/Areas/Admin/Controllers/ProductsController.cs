using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShredMates.Services.Admin.Interfaces;
using ShredMates.Web.Areas.Admin.Models.Products;
using ShredMates.Web.Controllers;
using ShredMates.Web.Infrastructure.Extensions;
using ShredMates.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShredMates.Web.Areas.Admin.Controllers
{
    public class ProductsController : AdminController
    {
        private readonly IProductService products;
        private readonly ICategoryService categories;

        public ProductsController(IProductService products, ICategoryService categories)
        {
            this.products = products;
            this.categories = categories;
        }

        public async Task<IActionResult> Create()
            => View(new CreateEditProductViewModel
            {
                CreatedDate = DateTime.UtcNow,
                Categories = await this.GetCategoriesAsync()
            });

        [HttpPost]
        public async Task<IActionResult> Create(CreateEditProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var product = this.products.ExistsByName(model.Title);

            if (!product)
            {
                model.Categories = await this.GetCategoriesAsync();

                await this.products.CreateAsync(
                    model.Title,
                    model.ShortDescription,
                    model.Description,
                    model.Price,
                    model.ImageUrl,
                    model.ImageThumbnailUrl,
                    model.CreatedDate,
                    model.CategoryId);
            }

            TempData.AddSuccessMessage($"Product {model.Title} created successfully!");

            return Redirect("/");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await this.products.FindByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(new CreateEditProductViewModel
            {
                Title = product.Title,
                ShortDescription = product.ShortDescription,
                Description = product.Description,
                Price = product.Price,
                ImageThumbnailUrl = product.ImageThumbnailUrl,
                ImageUrl = product.ImageUrl,
                CreatedDate = product.CreatedDate,
                CategoryId = product.CategoryId,
                Categories = await this.GetCategoriesAsync()
            });

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CreateEditProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var product = this.products.ExistsById(id);

            if (!product)
            {
                return NotFound();
            }

            model.Categories = await this.GetCategoriesAsync();

            await this.products.EditAsync(
                id,
                model.Title,
                model.ShortDescription,
                model.Description,
                model.Price,
                model.ImageThumbnailUrl,
                model.ImageUrl,
                model.CreatedDate,
                model.CategoryId);

            TempData.AddSuccessMessage($"Product {model.Title} updated successfully!");

            return Redirect("/");
        }

        public async Task<IActionResult> Delete(int id) => await Task.Run(() => View(id));

        public async Task<IActionResult> DeleteProduct(int id)
        {
            await this.products.DeleteAsync(id);

            TempData.AddWarningMessage($"Product deleted successfully!");

            return Redirect("/");
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
        }

        private async Task<IEnumerable<SelectListItem>> GetCategoriesAsync()
        {
            var categories = await this.categories.AllAsync();

            var categoriesList = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            })
            .ToList();

            return categoriesList;
        }
    }
}
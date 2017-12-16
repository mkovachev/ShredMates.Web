using AutoMapper;
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
        private readonly IMapper mapper;

        public CategoryController(ICategoryService categories, IMapper mapper)
        {
            this.categories = categories;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index(int id, int page = 1)
        {
            return View(new CategoryViewModel
            {
                Products = await this.categories.ProductsinCategoryAsync(id, page, DataConstants.PageSize),
                Current = page,
                TotalPages = (int)Math.Ceiling(this.categories.TotalPages() / (double)DataConstants.PageSize)
            });
        }

    }
}
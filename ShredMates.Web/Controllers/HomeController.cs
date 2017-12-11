﻿using Microsoft.AspNetCore.Mvc;
using ShredMates.Data;
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

        public HomeController(IProductService products)
        {
            this.products = products;
        }

        public async Task<IActionResult> Index(int page = 1)
            => View(new HomeViewModel
            {
                Products = await this.products.AllAsync(page, DataConstants.PageSize),
                Current = page,
                TotalPages = (int)Math.Ceiling(this.products.TotalPages() / (double)DataConstants.PageSize)
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

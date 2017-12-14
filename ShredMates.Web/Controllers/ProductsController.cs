using Microsoft.AspNetCore.Mvc;
using ShredMates.Services.Admin.Interfaces;
using ShredMates.Web.Models;
using System.Threading.Tasks;


namespace ShredMates.Web.Controllers
{
    public class ProductsController : Controller
    {

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

        }
    }
}
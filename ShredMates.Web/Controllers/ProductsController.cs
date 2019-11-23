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
            var product = await products.FindByIdAsync(id).ConfigureAwait(false);

            if (product == null)
            {
                return NotFound();
            }

            return await Task.Run(() => View(new ProductDetailsViewModel
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                Images = product.Images,
                ProductAttributes = product.ProductAttributes
            }))
                .ConfigureAwait(false);

        }
    }
}
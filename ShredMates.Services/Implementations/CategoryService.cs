using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ShredMates.Data;
using ShredMates.Data.Models;
using ShredMates.Services.Interfaces;
using ShredMates.Services.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShredMates.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ShredMatesDbContext db;
        private readonly ShoppingCart shoppingCart;

        public CategoryService(ShredMatesDbContext db, ShoppingCart shoppingCart)
        {
            this.db = db;
            this.shoppingCart = shoppingCart;
        }

        public async Task<Category> ByIdAsync(int id)
        {
            return await this.db.Categories.FindAsync(id);
        }

        public async Task<IEnumerable<AllProductsServiceModel>> ProductsinCategoryAsync(int categoryId, int page = 1, int pageSize = DataConstants.PageSize)
        {
            return await this.db
                    .Products
                    .Where(p => p.CategoryId == categoryId)
                    .OrderBy(p => p.Title)
                    .ProjectTo<AllProductsServiceModel>()
                    .ToListAsync();
        }

        // category does not contain a list of products in create method TODO
        //public async Task<CategoryServiceModel> AllInCategoryAsync(int id, int page = 1, int pageSize = DataConstants.PageSize)
        //{
        //    return await this.db
        //                .Categories
        //                .Where(c => c.Id == id)
        //                .Select(c => new CategoryServiceModel
        //                {
        //                    Products = c.Products.Select(p => new AllProductsServiceModel
        //                    {
        //                        Title = p.Title,
        //                        ShortDescription = p.ShortDescription,
        //                        ImageThumbnailUrl = p.ImageThumbnailUrl,
        //                        Price = p.Price
        //                    })
        //                })
        //                .Skip((page - 1) * pageSize)
        //                .Take(pageSize)
        //                .ProjectTo<CategoryServiceModel>()
        //                .FirstOrDefaultAsync();
        //}

        public int TotalPages()
            => this.db.Categories.Count();
    }
}

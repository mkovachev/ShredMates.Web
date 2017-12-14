using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ShredMates.Data;
using ShredMates.Data.Models;
using ShredMates.Services.Interfaces;
using ShredMates.Services.Models;

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

        public async Task<IEnumerable<CategoryServiceModel>> AllProductsInCategoryAsync(int page = 1, int pageSize = DataConstants.PageSize)
        {
            return await this.db
                        .Categories
                        .OrderByDescending(p => p.CreatedDate)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ProjectTo<CategoryServiceModel>()
                        .ToListAsync();
        }

        public int TotalPages()
            => this.db.Categories.Count();

    }
}

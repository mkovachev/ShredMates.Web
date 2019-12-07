using AutoMapper;
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
        private readonly IMapper mapper;

        public CategoryService(ShredMatesDbContext db, ShoppingCart shoppingCart, IMapper mapper)
        {
            this.db = db ?? throw new System.ArgumentNullException(nameof(db));
            this.shoppingCart = shoppingCart ?? throw new System.ArgumentNullException(nameof(shoppingCart));
            this.mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
        }

        public async Task<Category> ByIdAsync(int id)
        {
            return await this.db.Categories.FindAsync(id);
        }


        public async Task<IEnumerable<ProductListingServiceModel>> AllProductsInCategoryAsync(int categoryId, int page = 1, int pageSize = DataConstants.PageSize)
        {
            return await this.db
                    .Products
                    .Where(p => p.CategoryId == categoryId)
                    .OrderBy(p => p.Title)
                    .ProjectTo<ProductListingServiceModel>(this.mapper.ConfigurationProvider)
                    .ToListAsync();
        }

        public async Task<CategoryServiceModel> AllInCategoryAsync(int id, int page = 1, int pageSize = DataConstants.PageSize)
        {
            return await this.db
                        .Categories
                        .Where(c => c.Id == id)
                        .Select(c => new CategoryServiceModel
                        {
                            Products = c.Products.Select(p => new ProductListingServiceModel
                            {
                                Title = p.Title,
                                ShortDescription = p.ShortDescription,
                                Thumbnail = p.Thumbnail,
                                Price = p.Price
                            })
                        })
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ProjectTo<CategoryServiceModel>(this.mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();
        }

        public int TotalPages()
            => this.db.Categories.Count();
    }
}

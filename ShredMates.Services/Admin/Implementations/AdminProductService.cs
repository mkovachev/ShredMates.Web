using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ShredMates.Data;
using ShredMates.Data.Models;
using ShredMates.Services.Admin.Interfaces;
using ShredMates.Services.Admin.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShredMates.Services.Admin.Implementations
{
    public class AdminProductService : IAdminProductService
    {
        private readonly ShredMatesDbContext db;

        public AdminProductService(ShredMatesDbContext db)
        {
            this.db = db;
        }

        public async Task<AdminProductServiceModel> FindByIdAsync(int id)
            => await this.db
                 .Products
                 .Where(p => p.Id == id)
                 .ProjectTo<AdminProductServiceModel>()
                 .FirstOrDefaultAsync();

        public async Task CreateAsync(string title, string shortDescription, string description, decimal price, string imageUrl, string imageThumbnailUrl, DateTime createdDate, int categoryId)
        {
            var product = new Product
            {
                Title = title,
                ShortDescription = shortDescription,
                Description = description,
                Price = price,
                ImageUrl = imageUrl,
                ImageThumbnailUrl = imageThumbnailUrl,
                CreatedDate = createdDate,
                CategoryId = categoryId
            };

            this.db.Add(product);

            await this.db.SaveChangesAsync();
        }

        public async Task EditAsync(int id, string title, string shortDescription, string description, decimal price, string imageUrl, string imageThumbnailUrl, DateTime createdDate, int categoryId)
        {
            var product = await this.db.Products.FindAsync(id);

            if (product == null)
            {
                return;
            }
            product.Title = title;
            product.ShortDescription = shortDescription;
            product.Description = description;
            product.Price = price;
            product.ImageUrl = imageUrl;
            product.ImageThumbnailUrl = imageThumbnailUrl;
            product.CreatedDate = createdDate;
            product.CategoryId = categoryId;

            await this.db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await this.db.Products.FindAsync(id);

            if (product == null)
            {
                return;
            }

            this.db.Products.Remove(product);

            await this.db.SaveChangesAsync();
        }

        public bool ExistsById(int id)
        {
            return this.db.Products.Any(c => c.Id == id);
        }

        public bool ExistsByName(string title)
        {
            return this.db.Products.Any(c => c.Title == title);
        }

        public async Task<AdminProductServiceModel> DetailsAsync(int id) 
            => await this.db
                    .Products
                    .Where(p => p.Id == id)
                    .ProjectTo<AdminProductServiceModel>()
                    .FirstOrDefaultAsync();
    }
}

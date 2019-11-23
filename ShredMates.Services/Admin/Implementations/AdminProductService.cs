using Microsoft.EntityFrameworkCore;
using ShredMates.Data;
using ShredMates.Data.Models;
using ShredMates.Services.Admin.Interfaces;
using System;
using System.Collections.Generic;
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

        public async Task<Product> FindByIdAsync(int id)
            => await this.db
                 .Products
                 .Where(p => p.Id == id)
                 .FirstOrDefaultAsync();

        public bool ExistsById(int id)
        {
            return this.db.Products.Any(c => c.Id == id);
        }

        public bool ExistsByName(string title)
            => this.db.Products.Any(c => c.Title == title);

        public async Task<Product> DetailsAsync(int id)
            => await this.db
                    .Products
                    .Where(p => p.Id == id)
                    .FirstOrDefaultAsync();

        public async Task CreateAsync(string title, string shortDescription, string description, decimal price, List<Image> images, string thumbnail, List<ProductAttribute> productAttributes, DateTime createdDate, int categoryId)
        {
            var product = new Product
            {
                Title = title,
                ShortDescription = shortDescription,
                Description = description,
                Price = price,
                Images = images,
                Thumbnail = thumbnail,
                ProductAttributes = productAttributes,
                DateCreated = createdDate,
                CategoryId = categoryId
            };

            this.db.Products.Add(product);
            //this.db.Categories.Products.Add(product); // add to category products list TODO

            await this.db.SaveChangesAsync();
        }

        public async Task EditAsync(int id, string title, string shortDescription, string description, decimal price, List<Image> images, string thumbnail, List<ProductAttribute> productAttributes, DateTime createdDate, int categoryId)
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
            product.Images = images;
            product.Thumbnail = thumbnail;
            product.ProductAttributes = productAttributes;
            product.DateCreated = createdDate;
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
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShredMates.Data;
using ShredMates.Data.Models;
using ShredMates.Services.Admin.Interfaces;
using ShredMates.Services.Admin.Models;

namespace ShredMates.Services.Admin.Implementations
{
    public class AdminCategoryService : IAdminCategoryService
    {
        private readonly ShredMatesDbContext db;

        public AdminCategoryService(ShredMatesDbContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(string name)
        {
            var category = new Category
            {
                Name = name
            };

            this.db.Add(category);

            await this.db.SaveChangesAsync();
        }

        public async Task<IEnumerable<AdminCategoryServiceModel>> AllAsync() 
            => await this.db
                .Categories
                .OrderBy(c => c.Name)
                .Select(c => new AdminCategoryServiceModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();

        public async Task<AdminCategoryServiceModel> FindByIdAsync(int id)
        => await this.db
                 .Categories
                 .Where(c => c.Id == id)
                 .Select(c => new AdminCategoryServiceModel
                 {
                     Id = c.Id,
                     Name = c.Name,
                     
                 })
                 .FirstOrDefaultAsync();

        public async Task EditAsync(int id, string name)
        {
            var category = await this.db.Categories.FindAsync(id);

            if (category == null)
            {
                return;
            }
            category.Name = name;

            await this.db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await this.db.Categories.FindAsync(id);

            if (category == null)
            {
                return;
            }

            this.db.Categories.Remove(category);

            await this.db.SaveChangesAsync();
        }

        public bool ExistsById(int id)
        {
            return this.db.Categories.Any(c => c.Id == id);
        }

        public bool ExistsByName(string name)
        {
            return this.db.Categories.Any(c => c.Name == name);
        }
    }
}

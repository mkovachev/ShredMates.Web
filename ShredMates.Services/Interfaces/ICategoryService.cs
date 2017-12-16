using ShredMates.Data;
using ShredMates.Data.Models;
using ShredMates.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShredMates.Services.Interfaces
{
    public interface ICategoryService
    {
        // Task<CategoryServiceModel> AllInCategoryAsync(int id, int page = 1, int pageSize = DataConstants.PageSize);

        Task<Category> ByIdAsync(int id);

        Task<IEnumerable<AllProductsServiceModel>> ProductsinCategoryAsync(int categoryId, int page = 1, int pageSize = DataConstants.PageSize);

        int TotalPages();
    }
}

using ShredMates.Data;
using ShredMates.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShredMates.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryServiceModel>> AllProductsInCategoryAsync(int page = 1, int pageSize = DataConstants.PageSize);

        int TotalPages();
    }
}

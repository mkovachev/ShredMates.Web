using ShredMates.Data;
using ShredMates.Data.Models;
using ShredMates.Services.Common;
using ShredMates.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShredMates.Services.Interfaces
{
    public interface ICategoryService : ITransientService
    {
        Task<Category> ByIdAsync(int id);

        Task<IEnumerable<ProductListingServiceModel>> AllProductsInCategoryAsync(int categoryId, int page = 1, int pageSize = DataConstants.PageSize);

        int TotalPages();
    }
}

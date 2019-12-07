using ShredMates.Data;
using ShredMates.Data.Models;
using ShredMates.Services.Common;
using ShredMates.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShredMates.Services.Interfaces
{
    public interface IProductService : ITransientService
    {
        Task<IEnumerable<ProductListingServiceModel>> AllAsync(int page = 1, int pageSize = DataConstants.PageSize);

        int TotalPages();

        Task<Product> ByIdAsync(int id);

        Task<List<ProductListingServiceModel>> SearchAsync(string search);
    }
}

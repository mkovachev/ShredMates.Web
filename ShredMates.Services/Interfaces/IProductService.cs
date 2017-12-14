using ShredMates.Data;
using ShredMates.Data.Models;
using ShredMates.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShredMates.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<AllProductsServiceModel>> AllAsync(int page = 1, int pageSize = DataConstants.PageSize);

        int TotalPages();

        Task<Product> ByIdAsync(int id);

        Task<IEnumerable<AllProductsServiceModel>> FindAsync(string search);
    }
}

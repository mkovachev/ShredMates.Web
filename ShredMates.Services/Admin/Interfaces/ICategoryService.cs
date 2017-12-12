using ShredMates.Services.Admin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShredMates.Services.Admin.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryServiceModel> FindByIdAsync(int id);

        Task<IEnumerable<CategoryServiceModel>> AllAsync();

        Task CreateAsync(string name);

        Task EditAsync(int id, string name);

        Task DeleteAsync(int id);

        bool ExistsById(int id);

        bool ExistsByName(string name);
    }
}

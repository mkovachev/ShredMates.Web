using ShredMates.Services.Admin.Models;
using System;
using System.Threading.Tasks;

namespace ShredMates.Services.Admin.Interfaces
{
    public interface IAdminProductService
    {
        Task<AdminProductServiceModel> FindByIdAsync(int id);

        Task CreateAsync(
            string title,
            string shortDescription,
            string description,
            decimal price,
            string imageUrl,
            string imageThumbnailUrl,
            DateTime createdDate,
            int categoryId
            );

        Task EditAsync(
            int id,
            string title,
            string shortDescription,
            string description,
            decimal price,
            string imageUrl,
            string imageThumbnailUrl,
            DateTime createdDate,
            int categoryId
            );

        Task DeleteAsync(int id);

        Task<AdminProductServiceModel> DetailsAsync(int id);

        bool ExistsById(int id);

        bool ExistsByName(string title);

    }
}

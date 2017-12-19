using Microsoft.AspNetCore.Mvc.Rendering;
using ShredMates.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShredMates.Services.Admin.Interfaces
{
    public interface IAdminProductService
    {
        Task<Product> FindByIdAsync(int id);

        Task CreateAsync(
            string title,
            string shortDescription,
            string description,
            decimal price,
            List<Image> images,
            string thumbnail,
            List<ProductAttribute> productAttributes,
            DateTime createdDate,
            int categoryId
            );

        Task EditAsync(
            int id,
            string title,
            string shortDescription,
            string description,
            decimal price,
            List<Image> images,
            string thumbnail,
            List<ProductAttribute> productAttributes,
            DateTime createdDate,
            int categoryId
            );

        Task DeleteAsync(int id);

        Task<Product> DetailsAsync(int id);

        bool ExistsById(int id);

        bool ExistsByName(string title);

    }
}

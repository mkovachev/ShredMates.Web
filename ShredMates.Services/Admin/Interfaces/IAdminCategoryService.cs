﻿using ShredMates.Services.Admin.Models;
using ShredMates.Services.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShredMates.Services.Admin.Interfaces
{
    public interface IAdminCategoryService : ITransientService
    {
        Task<AdminCategoryServiceModel> FindByIdAsync(int id);

        Task<IEnumerable<AdminCategoryServiceModel>> AllAsync();

        Task CreateAsync(string name);

        Task EditAsync(int id, string name);

        Task DeleteAsync(int id);

        bool ExistsById(int id);

        bool ExistsByName(string name);
    }
}

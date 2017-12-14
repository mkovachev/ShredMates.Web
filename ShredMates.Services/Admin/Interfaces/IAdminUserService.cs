using ShredMates.Services.Admin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShredMates.Services.Admin.Interfaces
{
    public interface IAdminUserService
    {
        Task<IEnumerable<AdminUserServiceModel>> AllAsync();
    }
}

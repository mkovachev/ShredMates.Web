using ShredMates.Services.Admin.Models;
using ShredMates.Services.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShredMates.Services.Admin.Interfaces
{
    public interface IAdminUserService : ITransientService
    {
        Task<IEnumerable<AdminUserServiceModel>> AllAsync();
    }
}

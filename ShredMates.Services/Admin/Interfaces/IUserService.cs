using ShredMates.Services.Admin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShredMates.Services.Admin.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserServiceModel>> AllAsync();
    }
}

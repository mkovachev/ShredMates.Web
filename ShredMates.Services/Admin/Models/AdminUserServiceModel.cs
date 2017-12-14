using ShredMates.Common.Mapping;
using ShredMates.Data.Models;

namespace ShredMates.Services.Admin.Models
{
    public class AdminUserServiceModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string Email { get; set; }
    }
}

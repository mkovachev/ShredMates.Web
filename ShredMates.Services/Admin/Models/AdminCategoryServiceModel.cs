using ShredMates.Common.Mapping;
using ShredMates.Data.Models;

namespace ShredMates.Services.Admin.Models
{
    public class AdminCategoryServiceModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}

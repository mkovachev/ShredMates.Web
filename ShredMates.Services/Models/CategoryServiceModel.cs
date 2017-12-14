using ShredMates.Common.Mapping;
using ShredMates.Data.Models;
using System.Collections.Generic;

namespace ShredMates.Services.Models
{
    public class CategoryServiceModel: IMapFrom<Category>
    {
        IEnumerable<AllProductsServiceModel> Products { get; set; }
    }
}

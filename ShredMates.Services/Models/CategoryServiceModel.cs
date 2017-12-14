using ShredMates.Common.Mapping;
using ShredMates.Data.Models;
using System.Collections.Generic;

namespace ShredMates.Services.Models
{
    public class CategoryServiceModel: IMapFrom<Category>
    {
        public IEnumerable<AllProductsServiceModel> Products { get; set; }

        public Category CurrentCategory { get; set; }
    }
}

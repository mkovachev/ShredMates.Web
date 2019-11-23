using ShredMates.Common.Mapping;
using ShredMates.Data.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShredMates.Services.Models
{
    public class ProductDetailsServiceModel : IMapFrom<Product>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        [DataType(DataType.ImageUrl)]
        public List<Image> Images { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}

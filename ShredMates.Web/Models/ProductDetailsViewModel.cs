using ShredMates.Data.Models;
using System.Collections.Generic;

namespace ShredMates.Web.Models
{
    public class ProductDetailsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public List<Image> Images { get; set; }

        public List<ProductAttribute> ProductAttributes { get; set; }
    }
}

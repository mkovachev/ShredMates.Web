using ShredMates.Common.Mapping;
using ShredMates.Data.Models;

namespace ShredMates.Services.Models
{
    public class ProductListingServiceModel : IMapFrom<Product>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public string ShortDescription { get; set; }

        public string Thumbnail { get; set; }

    }
}

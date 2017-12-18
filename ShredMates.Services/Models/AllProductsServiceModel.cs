using ShredMates.Common.Mapping;
using ShredMates.Data.Models;

namespace ShredMates.Services.Models
{
    public class AllProductsServiceModel: IMapFrom<AllProductServiceModel>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public string ShortDescription { get; set; }

        public string ImageThumbnailUrl { get; set; }

    }
}

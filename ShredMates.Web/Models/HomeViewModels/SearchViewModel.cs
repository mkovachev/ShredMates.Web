using ShredMates.Services.Models;
using System.Collections.Generic;

namespace ShredMates.Web.Models.HomeViewModels
{
    public class SearchViewModel
    {
        public IEnumerable<ProductListingServiceModel> Products { get; set; }

        public string Search { get; set; }
    }
}

﻿using ShredMates.Services.Models;
using System.Collections.Generic;

namespace ShredMates.Web.Models.HomeViewModels
{
    public class ProductListingViewModel
    {
        public IEnumerable<ProductListingServiceModel> Products { get; set; }

        public string Search { get; set; }

        public int Current { get; set; }

        public int Previous => this.Current == 1 ? 1 : this.Current - 1;

        public int TotalPages { get; set; }

        public int Next => this.Current == TotalPages ? this.TotalPages : Current + 1;

    }
}

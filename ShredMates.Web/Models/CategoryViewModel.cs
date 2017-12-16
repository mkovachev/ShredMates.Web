﻿using ShredMates.Services.Models;

namespace ShredMates.Web.Models
{
    public class CategoryViewModel
    {
        public CategoryServiceModel Category { get; set; }

        public int Current { get; set; }

        public int Previous => this.Current == 1 ? 1 : this.Current - 1;

        public int TotalPages { get; set; }

        public int Next => this.Current == TotalPages ? this.TotalPages : Current + 1;

        public string Search { get; set; }

    }
}

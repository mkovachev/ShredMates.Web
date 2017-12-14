﻿using ShredMates.Data.Models;
using System;

namespace ShredMates.Services.Admin.Models
{
    public class AdminProductServiceModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public string ImageThumbnailUrl { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
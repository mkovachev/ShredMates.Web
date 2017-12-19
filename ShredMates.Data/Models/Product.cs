﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShredMates.Data.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MinLength(DataConstants.ProductTitleMinLength)]
        [MaxLength(DataConstants.ProductTitleMaxLength)]
        public string Title { get; set; }

        [MaxLength(DataConstants.ProductShortDescriptionMaxLength)]
        public string ShortDescription { get; set; }

        [MinLength(DataConstants.ProductDescriptionMinLength)]
        [MaxLength(DataConstants.ProductDescriptionMaxLength)]
        public string Description { get; set; }

        [Range(0, 10000)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [MinLength(10)]
        [MaxLength(2000)]
        [DataType(DataType.ImageUrl)]
        public string Thumbnail { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.mm.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }

        public List<Image> Images { get; set; }

        public List<ProductAttribute> ProductAttributes { get; set; }

        public int CategoryId { get; set; }

        //public virtual Category Category { get; set; }
        
    }
}

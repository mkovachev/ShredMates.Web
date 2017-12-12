using Microsoft.AspNetCore.Mvc.Rendering;
using ShredMates.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShredMates.Web.Areas.Admin.Models.Products
{
    public class CreateEditProductViewModel
    {
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
        //[DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }

        [MinLength(10)]
        [MaxLength(2000)]
        [Display(Name = "Image")]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        [MinLength(10)]
        [MaxLength(2000)]
        [Display(Name = "Thumbnail")]
        [DataType(DataType.ImageUrl)]
        public string ImageThumbnailUrl { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}

using System;
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
        public string ImageUrl { get; set; }

        [MinLength(10)]
        [MaxLength(2000)]
        [DataType(DataType.ImageUrl)]
        public string ImageThumbnailUrl { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        
    }
}

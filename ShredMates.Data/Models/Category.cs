using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShredMates.Data.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MinLength(DataConstants.CategoryNameMinLength)]
        [MaxLength(DataConstants.CategoryNameMaxLength)]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}

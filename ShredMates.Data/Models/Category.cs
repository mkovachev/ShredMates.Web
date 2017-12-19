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

        public List<Product> Products { get; set; }
    }
}

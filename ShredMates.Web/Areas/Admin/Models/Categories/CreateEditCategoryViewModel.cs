using ShredMates.Data;
using System.ComponentModel.DataAnnotations;

namespace ShredMates.Web.Areas.Admin.Models.Categories
{
    public class CreateEditCategoryViewModel
    {
        [Required]
        [MinLength(DataConstants.CategoryNameMinLength)]
        [MaxLength(DataConstants.CategoryNameMaxLength)]
        public string Name { get; set; }

    }
}

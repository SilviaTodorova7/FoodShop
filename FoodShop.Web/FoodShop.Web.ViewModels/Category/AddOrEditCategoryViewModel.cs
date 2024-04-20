using System.ComponentModel.DataAnnotations;
using static FoodShop.Common.EntityValidationConstants.Category;

namespace FoodShop.Web.ViewModels.Category
{
    public class AddOrEditCategoryViewModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = "Category name must be between {2} and {1} characters long!")]
        public string Name { get; set; } = null!;
    }
}

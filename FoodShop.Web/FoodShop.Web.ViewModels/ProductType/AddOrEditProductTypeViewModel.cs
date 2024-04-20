using System.ComponentModel.DataAnnotations;
using static FoodShop.Common.EntityValidationConstants.ProductType;

namespace FoodShop.Web.ViewModels.ProductType
{
    public class AddOrEditProductTypeViewModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = "Product type name must be between {2} and {1} characters long!")]
        public string Name { get; set; } = null!;
    }
}

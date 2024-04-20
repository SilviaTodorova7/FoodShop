using System.ComponentModel.DataAnnotations;
using static FoodShop.Common.EntityValidationConstants.TradeMark;

namespace FoodShop.Web.ViewModels.TradeMark
{
    public class AddOrEditTradeMarkViewModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;
    }
}
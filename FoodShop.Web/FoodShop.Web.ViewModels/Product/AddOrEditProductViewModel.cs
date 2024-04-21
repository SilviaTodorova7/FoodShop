using FoodShop.Web.ViewModels.ProductType;
using FoodShop.Web.ViewModels.Category;
using FoodShop.Web.ViewModels.TradeMark;
using System.ComponentModel.DataAnnotations;
using static FoodShop.Common.EntityValidationConstants.Product;

namespace FoodShop.Web.ViewModels.Product
{
    public class AddOrEditProductViewModel
    {
        public AddOrEditProductViewModel()
        {
            this.ProductTypes = new HashSet<ProductTypeViewModel>();
            this.TradeMarks = new HashSet<TradeMarkViewModel>();
            this.Categories = new HashSet<CategoryViewModel>();
        }
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        public int CategoryId { get; set; }

        public ICollection<CategoryViewModel> Categories { get; set; }
        
        [Required]
        public int ProductTypeId { get; set; }

        public ICollection<ProductTypeViewModel> ProductTypes { get; set; }
        
        public int? TradeMarkId { get; set; }

        public ICollection<TradeMarkViewModel>? TradeMarks { get; set; }

        [Required]
        public string PictureUrl { get; set; } = null!;

        [Range(typeof(decimal), PriceMinValue, PriceMaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range (QuantityMinValue, QuantityMaxValue)]
        public int Quantity { get; set; }
    }
}

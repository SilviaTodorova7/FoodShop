using FoodShop.Web.ViewModels.ProductType;
using FoodShop.Web.ViewModels.Category;
using FoodShop.Web.ViewModels.TradeMark;

namespace FoodShop.Web.ViewModels.Product
{
    public class AddOrEditProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int CategoryId { get; set; }

        public ICollection<CategoryViewModel> Categories { get; set; } = null!;

        public int ProductTypeId { get; set; }

        public ICollection<ProductTypeViewModel> ProductTypes { get; set; } = null!;

        public int? TradeMarkId { get; set; }

        public ICollection<TradeMarkViewModel>? TradeMarks { get; set; }

        public string PictureUrl { get; set; } = null!;

        public string Price { get; set; } = null!;

        public int Quantity { get; set; }
    }
}

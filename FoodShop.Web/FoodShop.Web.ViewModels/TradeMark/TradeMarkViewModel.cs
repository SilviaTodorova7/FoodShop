using FoodShop.Web.ViewModels.Product;

namespace FoodShop.Web.ViewModels.TradeMark
{
    public class TradeMarkViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public ICollection<ProductFromTradeMarkViewModel> Products { get; set; } = null!;
    }
}

namespace FoodShop.Web.ViewModels.CartItem
{
    public class CartItemViewModel
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public int Count { get; set; }
    }
}

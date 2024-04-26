using FoodShop.Data.Models;

namespace FoodShop.Web.ViewModels.Product
{
    public class CartProductViewModel
    {
        public string UserId { get; set; } = null!;

        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public int Count { get; set; }

        public decimal ProductPrice { get; set; }

    }
}

using FoodShop.Data.Models;
using FoodShop.Web.ViewModels.CartItem;

namespace FoodShop.Web.ViewModels.Cart
{
    public class CartViewModel
    {
        public CartViewModel()
        {
            this.CartItems = new HashSet<CartItemViewModel>();
        }

        public ICollection<CartItemViewModel> CartItems { get; set; }

        public int Totalprice { get; set; }
    }
}

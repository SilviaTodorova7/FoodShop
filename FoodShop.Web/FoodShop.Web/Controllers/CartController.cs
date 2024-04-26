using FoodShop.Services;
using FoodShop.Services.Interfaces;
using FoodShop.Web.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FoodShop.Web.Infrastructure.Extensions.ClaimsPrincipalExtensions;

namespace FoodShop.Web.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        public async Task<IActionResult> Cart()
        {
            string userId = User.GetUserId();
            ICollection<CartProductViewModel> model = await this.cartService.GoToCart(userId);

            return View(model);
        }
    }
}

using FoodShop.Services;
using FoodShop.Services.Interfaces;
using FoodShop.Web.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FoodShop.Web.Infrastructure.Extensions.ClaimsPrincipalExtensions;
using static FoodShop.Common.NotificationMessagesConstants;

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

        public async Task<IActionResult> MyCart()
        {
            string userId = User.GetUserId();

            try
            {
                ICollection<CartProductViewModel> model = await this.cartService.GoToCart(userId);

                return View(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Unexpected error occured while trying to reach your Cart. Please try again later or contact administrator!");
                return RedirectToAction("All", "Product");
            }
        }

        public async Task<IActionResult>BuyProducts()
        {
            string userId = this.User.GetUserId();

            try
            {
                string result = await this.cartService.BuyProductsAsync(userId);

                if (result == string.Empty)
                {
                    this.TempData[SuccessMessage] = "You have successfully bought Products!";

                    return RedirectToAction("MyCart", "Cart");
                }
                this.TempData[WarningMessage] = $"Sorry, we have only {result} Products! Please correct Your Cart!";

                return RedirectToAction("MyCart", "Cart");

            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Unexpected error occured while Buying Products. Please try again later or contact administrator!");
                return RedirectToAction("All", "Product");
            }
        }
    }
}

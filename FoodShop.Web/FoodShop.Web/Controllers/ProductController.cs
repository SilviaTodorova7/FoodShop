using FoodShop.Services.Interfaces;
using FoodShop.Web.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;

namespace FoodShop.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await this.productService.GetAllProductsAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            ProductDetailsViewModel model = await this.productService.GetProductDetailsAsync(id);

            return View(model);
        }
    }
}

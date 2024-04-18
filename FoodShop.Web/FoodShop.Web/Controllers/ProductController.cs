using FoodShop.Services.Interfaces;
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
        public async Task<IActionResult> All()
        {
            var model = await this.productService.GetAllProductsAsync();

            return View(model);
        }
    }
}

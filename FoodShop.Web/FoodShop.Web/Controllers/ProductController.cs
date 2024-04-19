using FoodShop.Services.Interfaces;
using FoodShop.Web.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodShop.Web.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly IProductTypeService productTypeService;
        private readonly ITradeMarkService tradeMarkService;
        public ProductController(IProductService productService, ICategoryService categoryService, IProductTypeService productTypeService, ITradeMarkService tradeMarkService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.productTypeService = productTypeService;
            this.tradeMarkService = tradeMarkService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await this.productService.GetAllProductsAsync();

            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            ProductDetailsViewModel model = await this.productService.GetProductDetailsAsync(id);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add(int id)
        {
            try
            {
                AddOrEditProductViewModel model = await this.productService
                    .GetProductForAddOrEditAsync(id);
                model.Categories = await this.categoryService.GetAllCategoriesAsync();
                model.ProductTypes = await this.productTypeService.GetAllProductTypesAsync();
                model.TradeMarks = await this.tradeMarkService.GetAllTradeMarksAsync();

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("All", "Product");
            }

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                AddOrEditProductViewModel model = await this.productService
                    .GetProductForAddOrEditAsync(id);
                model.Categories = await this.categoryService.GetAllCategoriesAsync();
                model.ProductTypes = await this.productTypeService.GetAllProductTypesAsync();
                model.TradeMarks = await this.tradeMarkService.GetAllTradeMarksAsync();
                
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("All", "Product");
            }
            
        }
    }
}

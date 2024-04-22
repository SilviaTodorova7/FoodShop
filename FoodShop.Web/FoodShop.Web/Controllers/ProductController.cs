using FoodShop.Services.Interfaces;
using FoodShop.Services.Models.Product;
using FoodShop.Web.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FoodShop.Common.NotificationMessagesConstants;

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

        //[AllowAnonymous]
        //[HttpGet]
        //public async Task<IActionResult> All()
        //{
        //    var model = await this.productService.GetAllProductsAsync();

        //    return View(model);
        //}

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> All([FromQuery]AllProductQueryModel queryModel)
        {
            AllProductsFilteredAndPagedServiceModel serviceModel = await this.productService
                .AllAsync(queryModel);

            queryModel.Products = serviceModel.Products;
            queryModel.TotalProducts = serviceModel.TotalProducts;
            queryModel.Categories = await this.categoryService.GetAllCategoriesNamesAsync();
            queryModel.TradeMarks = await this.tradeMarkService.GetAllTradeMarksNamesAsync();
            queryModel.ProductTypes = await this.productTypeService.GetAllProductTypesNamesAsync();

            return View(queryModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            ProductDetailsViewModel model = await this.productService.GetProductDetailsAsync(id);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            try
            {
                AddOrEditProductViewModel model = new AddOrEditProductViewModel()
                {
                    Categories = await this.categoryService.GetAllCategoriesAsync(),
                    ProductTypes = await this.productTypeService.GetAllProductTypesAsync(),
                    TradeMarks = await this.tradeMarkService.GetAllTradeMarksAsync(),
                };
                
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("All", "Product");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Add(AddOrEditProductViewModel model)
        {
            bool categoryExist = await this.categoryService.CategoryExistsByIdAsync(model.CategoryId);
            if (!categoryExist)
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Category doesn't exist!");
            }

            bool productTypeExist = await this.productTypeService.ProductTypeExistsByIdAsync(model.ProductTypeId);
            if (!productTypeExist)
            {
                ModelState.AddModelError(nameof(model.ProductTypeId), "Product type doesn't exist!");
            }

            if (model.TradeMarkId != null)
            {
                bool tradeMarkExist = await this.tradeMarkService.TradeMarkExistsByIdAsync(model.TradeMarkId);
                if (!tradeMarkExist)
                {
                    ModelState.AddModelError(nameof(model.TradeMarkId), "Trademark doesn't exist!");
                }
            }

            bool existByName = await this.productService.ProductExistByName(model.Name);
            if (existByName)
            {
                this.TempData[ErrorMessage] = "Product with this name already exist.";
                ModelState.AddModelError(string.Empty, "Product with this name already exist.");
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await this.categoryService.GetAllCategoriesAsync();
                model.TradeMarks = await this.tradeMarkService.GetAllTradeMarksAsync();
                model.ProductTypes = await this.productTypeService.GetAllProductTypesAsync();

                return View(model);
            }

            try
            {
                await this.productService.AddProductAsync(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Unexpected error occured while adding product. Please try again later or contact administrator!");
                model.Categories = await this.categoryService.GetAllCategoriesAsync();
                model.TradeMarks = await this.tradeMarkService.GetAllTradeMarksAsync();
                model.ProductTypes = await this.productTypeService.GetAllProductTypesAsync();

                return View(model);

            }

            return RedirectToAction("All", "Product");

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                AddOrEditProductViewModel model = await this.productService
                    .GetProductForEditAsync(id);
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

        [HttpPost]
        public async Task<IActionResult> Edit(AddOrEditProductViewModel model, int id)
        {

            bool categoryExist = await this.categoryService.CategoryExistsByIdAsync(model.CategoryId);
            if (!categoryExist)
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Category doesn't exist!");
            }

            bool productTypeExist = await this.productTypeService.ProductTypeExistsByIdAsync(model.ProductTypeId);
            if (!productTypeExist)
            {
                ModelState.AddModelError(nameof(model.ProductTypeId), "Product type doesn't exist!");
            }

            if (model.TradeMarkId != null)
            {
                bool tradeMarkExist = await this.tradeMarkService.TradeMarkExistsByIdAsync(model.TradeMarkId);
                if (!tradeMarkExist)
                {
                    ModelState.AddModelError(nameof(model.TradeMarkId), "Trademark doesn't exist!");
                }
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await this.categoryService.GetAllCategoriesAsync();
                model.TradeMarks = await this.tradeMarkService.GetAllTradeMarksAsync();
                model.ProductTypes = await this.productTypeService.GetAllProductTypesAsync();

                return View(model);
            }

            try
            {
                await this.productService.EditProductAsync(model, id);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Unexpected error occured while editing product. Please try again later or contact administrator!");
                model.Categories = await this.categoryService.GetAllCategoriesAsync();
                model.TradeMarks = await this.tradeMarkService.GetAllTradeMarksAsync();
                model.ProductTypes = await this.productTypeService.GetAllProductTypesAsync();

                return View(model);

            }

            return RedirectToAction("All", "Product");

        }

    }
}

using FoodShop.Data.Models;
using FoodShop.Services.Interfaces;
using FoodShop.Services.Models.Product;
using FoodShop.Web.Infrastructure.Extensions;
using FoodShop.Web.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
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

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> All([FromQuery] AllProductQueryModel queryModel)
        {
            try
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
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Unexpected error occured. Please try again later or contact administrator!");
                this.TempData[ErrorMessage] = "Unexpected error occured. Please try again later or contact administrator!";

                return BadRequest();
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            bool existById = await this.productService.ExistByIdAsync(id);

            if (!existById)
            {
                this.TempData[ErrorMessage] = "Product with the provided id does not exist!";

                return RedirectToAction("All", "Product");
            }

            try
            {
                ProductDetailsViewModel model = await this.productService.GetProductDetailsAsync(id);

                return View(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Unexpected error occured while trying to view product details. Please try again later or contact administrator!");
                this.TempData[ErrorMessage] = "Unexpected error occured while trying to view product details. Please try again later or contact administrator!";

                return RedirectToAction("All", "Product");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            if (User.IsAdmin())
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
                    ModelState.AddModelError(string.Empty, "Unexpected error occured while trying to add product. Please try again later or contact administrator!");
                    this.TempData[ErrorMessage] = "Unexpected error occured while trying to add product. Please try again later or contact administrator!";

                    return RedirectToAction("All", "Product");
                }

            }

            this.TempData[WarningMessage] = "You have to be administrator to reach this page!";
            return RedirectToAction("All", "Product");
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddOrEditProductViewModel model)
        {
            if (User.IsAdmin())
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
                    model.Categories = await this.categoryService.GetAllCategoriesAsync();
                    model.ProductTypes = await this.productTypeService.GetAllProductTypesAsync();
                    model.TradeMarks = await this.tradeMarkService.GetAllTradeMarksAsync();

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
                    this.TempData[SuccessMessage] = "You have added product successfully!";

                    return RedirectToAction("All", "Product");
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "Unexpected error occured while adding product. Please try again later or contact administrator!");
                    this.TempData[ErrorMessage] = "Unexpected error occured while adding product. Please try again later or contact administrator!";

                    return RedirectToAction("Äll", "Product");
                }

            }

            this.TempData[WarningMessage] = "You have to be administrator to reach this page!";
            return RedirectToAction("All", "Product");

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (User.IsAdmin())
            {
                bool existById = await this.productService.ExistByIdAsync(id);

                if (!existById)
                {
                    this.TempData[ErrorMessage] = "Product with the provided id does not exist!";

                    return RedirectToAction("All", "Product");
                }

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
                    ModelState.AddModelError(string.Empty, "Unexpected error occured while trying to edit product. Please try again later or contact administrator!");
                    this.TempData[ErrorMessage] = "Unexpected error occured while trying to edit product. Please try again later or contact administrator!";

                    return RedirectToAction("All", "Product");
                }

            }

            this.TempData[WarningMessage] = "You have to be administrator to reach this page!";
            return RedirectToAction("All", "Product");

        }

        [HttpPost]
        public async Task<IActionResult> Edit(AddOrEditProductViewModel model, int id)
        {
            if (User.IsAdmin())
            {
                bool existById = await this.productService.ExistByIdAsync(id);

                if (!existById)
                {
                    this.TempData[ErrorMessage] = "Product with the provided id does not exist!";

                    return RedirectToAction("All", "Product");
                }

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
                    this.TempData[SuccessMessage] = "You have edited product successfully!";

                    return RedirectToAction("Details", "Product", new { id });
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "Unexpected error occured while editing product. Please try again later or contact administrator!");
                    this.TempData[ErrorMessage] = "Unexpected error occured while editing product. Please try again later or contact administrator!";

                    return RedirectToAction("All", "Product");
                }

            }

            this.TempData[WarningMessage] = "You have to be administrator to reach this page!";
            return RedirectToAction("All", "Product");

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (User.IsAdmin())
            {
                bool existById = await this.productService.ExistByIdAsync(id);

                if (!existById)
                {
                    this.TempData[ErrorMessage] = "Product with the provided id does not exist!";

                    return RedirectToAction("All", "Product");
                }

                try
                {
                    DeleteProductViewModel model = await this.productService
                        .GetProductViewDetailsToDeleteAsync(id);

                    return View(model);
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "Unexpected error occured while trying to delete product. Please try again later or contact administrator!");
                    this.TempData[ErrorMessage] = "Unexpected error occured while trying to delete product. Please try again later or contact administrator!";

                    return RedirectToAction("All", "Product");
                }

            }

            this.TempData[WarningMessage] = "You have to be administrator to reach this page!";
            return RedirectToAction("All", "Product");

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, DeleteProductViewModel model)
        {
            if (User.IsAdmin())
            {
                bool existById = await this.productService.ExistByIdAsync(id);

                if (!existById)
                {
                    this.TempData[ErrorMessage] = "Product with the provided id does not exist!";

                    return RedirectToAction("All", "Product");
                }

                try
                {
                    await this.productService.DeleteProductByIdAsync(id, model);
                    this.TempData[WarningMessage] = "You have successfully deleted product!";

                    return RedirectToAction("All", "Product");
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "Unexpected error occured while deleting product. Please try again later or contact administrator!");
                    this.TempData[ErrorMessage] = "Unexpected error occured while deleting product. Please try again later or contact administrator!";

                    return RedirectToAction("All", "Product");
                }

            }

            this.TempData[WarningMessage] = "You have to be administrator to reach this page!";
            return RedirectToAction("All", "Product");

        }


        public async Task<IActionResult> AddToCartAsync(int id)
        {
            string userId = this.User.GetUserId();

            try
            {
                bool result = await this.productService.AddProductToCartAsync(id, userId);

                if (result == true)
                {
                    this.TempData[SuccessMessage] = "You have successfully added product to Cart!";
                }
                else
                {
                    this.TempData[WarningMessage] = "Sorry, we don't have any more of this Product!";
                }
                return RedirectToAction("MyCart", "Cart");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Unexpected error occured while adding product to Cart. Please try again later or contact administrator!");
                return RedirectToAction("All", "Product");
            }
        }

        public async Task<IActionResult> RemoveFromCartAsync(int id)
        {
            string userId = this.User.GetUserId();

            try
            {
                await this.productService.RemoveProductFromCartAsync(id, userId);
                this.TempData[WarningMessage] = "You have successfully removed product from Cart!";
                return RedirectToAction("MyCart", "Cart");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Unexpected error occured while removing product to Cart. Please try again later or contact administrator!");
                return RedirectToAction("All", "Product");
            }
        }
    }
}

using FoodShop.Data.Models;
using FoodShop.Services.Interfaces;
using FoodShop.Web.ViewModels.ProductType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FoodShop.Common.NotificationMessagesConstants;

namespace FoodShop.Web.Controllers
{
    [Authorize]
    public class ProductTypeController : Controller
    {
        private readonly IProductTypeService productTypeService;
        public ProductTypeController(IProductTypeService productTypeService)
        {
            this.productTypeService = productTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            ICollection<ProductTypeViewModel> model = await this.productTypeService
                .GetAllProductTypesAsync();

            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            AddOrEditProductTypeViewModel model = new AddOrEditProductTypeViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddOrEditProductTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await this.productTypeService.AddProductTypeAsync(model);
                this.TempData[SuccessMessage] = "You have added Product Type successfully!";

                return RedirectToAction("All", "ProductType");
            }
            catch (Exception)
            {
                ModelState.AddModelError(nameof(ProductType), "Something went wrond while adding product type. Please try again later or contact administrator!");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            AddOrEditProductTypeViewModel model = await this.productTypeService
                .GetProductTypeForEditAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddOrEditProductTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await this.productTypeService.EditProductTypeAsync(id, model);
                this.TempData[SuccessMessage] = "You have edited Product Type successfully!";

                return RedirectToAction("All", "ProductType");
            }
            catch (Exception)
            {
                ModelState.AddModelError(nameof(ProductType), "Something went wrond while editing product type. Please try again later or contact administrator!");
                return View(model);
            }
        }
    }
}

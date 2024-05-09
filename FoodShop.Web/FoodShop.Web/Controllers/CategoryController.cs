using FoodShop.Data.Models;
using FoodShop.Services.Interfaces;
using FoodShop.Web.Infrastructure.Extensions;
using FoodShop.Web.ViewModels.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FoodShop.Common.NotificationMessagesConstants;

namespace FoodShop.Web.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            ICollection<CategoryViewModel> allCategories =
                await this.categoryService.GetAllCategoriesAsync();

            return View(allCategories);
        }

        [HttpGet]
        public IActionResult Add()
        {
            if (User.IsAdmin())
            {
                AddOrEditCategoryViewModel model = new AddOrEditCategoryViewModel();

                return View(model);
            }

            this.TempData[WarningMessage] = "You have to be administrator to reach this page!";
            return RedirectToAction("All", "Product");
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddOrEditCategoryViewModel model)
        {
            if (User.IsAdmin())
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                try
                {
                    await this.categoryService.AddCategoryAsync(model);
                    this.TempData[SuccessMessage] = "You have added new Category successfully!";

                    return RedirectToAction("All", "Category");
                }
                catch (Exception)
                {
                    this.TempData[ErrorMessage] = "Something went wrong while adding category. Please try again later or contact administrator!";
                    ModelState.AddModelError(nameof(Category), "Something went wrong while adding category. Please try again later or contact administrator!");
                    return View(model);
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
                try
                {
                    AddOrEditCategoryViewModel model = await this.categoryService
                        .GetCategoryForEditAsync(id);

                    return View(model);
                }
                catch (Exception)
                {
                    return RedirectToAction("All", "Category");
                }
            }

            this.TempData[WarningMessage] = "You have to be administrator to reach this page!";
            return RedirectToAction("All", "Product");

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddOrEditCategoryViewModel model)
        {
            if (User.IsAdmin())
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                try
                {
                    await this.categoryService.EditCategoryAsync(id, model);
                    this.TempData[SuccessMessage] = "You have edited Category successfully!";

                    return RedirectToAction("All", "Category");
                }
                catch (Exception)
                {
                    this.TempData[ErrorMessage] = "Something went wrong while editing category. Please try again later or contact administrator!";
                    ModelState.AddModelError(nameof(Category), "Something went wrong while editing category. Please try again later or contact administrator!");
                    return View(model);
                }
            }

            this.TempData[WarningMessage] = "You have to be administrator to reach this page!";
            return RedirectToAction("All", "Product");
        }
    }
}

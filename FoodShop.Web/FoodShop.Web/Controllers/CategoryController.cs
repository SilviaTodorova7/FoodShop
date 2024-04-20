using FoodShop.Data.Models;
using FoodShop.Services.Interfaces;
using FoodShop.Web.ViewModels.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> All()
        {
            ICollection<CategoryViewModel> allCategories =
                await this.categoryService.GetAllCategoriesAsync();

            return View(allCategories);
        }

        [HttpGet]
        public IActionResult Add()
        {
            AddOrEditCategoryViewModel model = new AddOrEditCategoryViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddOrEditCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await this.categoryService.AddCategoryAsync(model);
                return RedirectToAction("All", "Category");
            }
            catch (Exception)
            {
                ModelState.AddModelError(nameof(Category), "Something went wrong while adding category. Please try again later or contact administrator!");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
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

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddOrEditCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await this.categoryService.EditCategoryAsync(id, model);

            return RedirectToAction("All", "Category");
        }
    }
}

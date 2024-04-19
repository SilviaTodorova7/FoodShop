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
    }
}

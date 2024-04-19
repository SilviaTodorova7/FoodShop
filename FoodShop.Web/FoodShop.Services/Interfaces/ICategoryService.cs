using FoodShop.Web.ViewModels.Category;

namespace FoodShop.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ICollection<CategoryViewModel>> GetAllCategoriesAsync();
    }
}

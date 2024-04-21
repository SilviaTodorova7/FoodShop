using FoodShop.Web.ViewModels.Category;

namespace FoodShop.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ICollection<CategoryViewModel>> GetAllCategoriesAsync();

        Task AddCategoryAsync(AddOrEditCategoryViewModel model);

        Task<AddOrEditCategoryViewModel> GetCategoryForEditAsync(int id);

        Task EditCategoryAsync(int id, AddOrEditCategoryViewModel model);

        Task<bool> CategoryExistsByIdAsync(int id);
    }
}

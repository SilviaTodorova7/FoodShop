using FoodShop.Web.ViewModels.Category;
using FoodShop.Web.ViewModels.Product;

namespace FoodShop.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ICollection<CategoryViewModel>> GetAllCategoriesAsync();

        Task<ICollection<string>> GetAllCategoriesNamesAsync();

        Task AddCategoryAsync(AddOrEditCategoryViewModel model);

        Task<AddOrEditCategoryViewModel> GetCategoryForEditAsync(int id);

        Task EditCategoryAsync(int id, AddOrEditCategoryViewModel model);

        Task<bool> CategoryExistsByIdAsync(int id);

        Task<ICollection<ProductFromCategoryViewModel>> GetProductsFromCategoryAsync(int id);

        Task<CategoryViewModel> GetCategoryAndProductsAsync(int id);
    }
}

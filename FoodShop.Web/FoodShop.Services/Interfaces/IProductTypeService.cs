using FoodShop.Web.ViewModels.Product;
using FoodShop.Web.ViewModels.ProductType;

namespace FoodShop.Services.Interfaces
{
    public interface IProductTypeService
    {
        Task<ICollection<ProductTypeViewModel>> GetAllProductTypesAsync();

        Task<ICollection<string>> GetAllProductTypesNamesAsync();

        Task AddProductTypeAsync(AddOrEditProductTypeViewModel model);

        Task<AddOrEditProductTypeViewModel> GetProductTypeForEditAsync(int id);

        Task EditProductTypeAsync(int id, AddOrEditProductTypeViewModel model);

        Task<bool> ProductTypeExistsByIdAsync(int id);

        Task<ProductTypeViewModel> GetProductTypeAndProductsAsync(int id);

        Task<ICollection<ProductFromProductTypeViewModel>> GetProductsFromProductTypeAsync(int id);
    }
}

using FoodShop.Web.ViewModels.ProductType;

namespace FoodShop.Services.Interfaces
{
    public interface IProductTypeService
    {
        Task<ICollection<ProductTypeViewModel>> GetAllProductTypesAsync();

        Task AddProductTypeAsync(AddOrEditProductTypeViewModel model);

        Task<AddOrEditProductTypeViewModel> GetProductTypeForEditAsync(int id);

        Task EditProductTypeAsync(int id, AddOrEditProductTypeViewModel model);
    }
}

using FoodShop.Web.ViewModels.ProductType;

namespace FoodShop.Services.Interfaces
{
    public interface IProductTypeService
    {
        Task<ICollection<ProductTypeViewModel>> GetAllProductTypesAsync();
    }
}

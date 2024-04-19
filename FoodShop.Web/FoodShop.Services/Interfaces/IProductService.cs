using FoodShop.Web.ViewModels.Product;

namespace FoodShop.Services.Interfaces
{
    public interface IProductService
    {
        Task<ICollection<AllProductsViewModel>> GetAllProductsAsync();

        Task<ProductDetailsViewModel> GetProductDetailsAsync(int id);

        Task<AddOrEditProductViewModel> GetProductForAddOrEditAsync(int id);
    }
}

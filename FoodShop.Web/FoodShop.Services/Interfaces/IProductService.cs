using FoodShop.Services.Models.Product;
using FoodShop.Web.ViewModels.Product;

namespace FoodShop.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductDetailsViewModel> GetProductDetailsAsync(int id);

        Task<AddOrEditProductViewModel> GetProductForEditAsync(int id);

        Task AddProductAsync(AddOrEditProductViewModel model);

        Task<bool> ProductExistByName(string name);

        Task EditProductAsync(AddOrEditProductViewModel model, int id);

        Task<AllProductsFilteredAndPagedServiceModel> AllAsync(AllProductQueryModel model);

        Task<bool> ExistByIdAsync(int id);

        Task<DeleteProductViewModel> GetProductViewDetailsToDeleteAsync(int id);

        Task DeleteProductByIdAsync(int id, DeleteProductViewModel model);

        Task AddProductToCartAsync(int id, string userId);

        Task RemoveProductFromCartAsync(int id, string userId);
    }
}

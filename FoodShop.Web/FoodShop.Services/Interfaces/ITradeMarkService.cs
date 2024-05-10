using FoodShop.Web.ViewModels.Product;
using FoodShop.Web.ViewModels.TradeMark;

namespace FoodShop.Services.Interfaces
{
    public interface ITradeMarkService
    {
        Task<ICollection<TradeMarkViewModel>> GetAllTradeMarksAsync();

        Task<ICollection<string>> GetAllTradeMarksNamesAsync();

        Task AddTradeMarkAsync(AddOrEditTradeMarkViewModel model);

        Task<AddOrEditTradeMarkViewModel> GetForEditTradeMarkAsync(int id);

        Task EditTradeMarkAsync(int id, AddOrEditTradeMarkViewModel model);

        Task<bool> TradeMarkExistsByIdAsync(int? id);

        Task<ICollection<ProductFromTradeMarkViewModel>> GetProductsFromTradeMarkAsync(int id);

        Task<TradeMarkViewModel> GetTradeMarkAndProductsAsync(int id);
    }
}

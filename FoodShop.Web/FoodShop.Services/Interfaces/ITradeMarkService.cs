using FoodShop.Web.ViewModels.TradeMark;

namespace FoodShop.Services.Interfaces
{
    public interface ITradeMarkService
    {
        Task<ICollection<TradeMarkViewModel>> GetAllTradeMarksAsync();

        Task AddTradeMarkAsync(AddOrEditTradeMarkViewModel model);

        Task<AddOrEditTradeMarkViewModel> GetForEditTradeMarkAsync(int id);

        Task EditTradeMarkAsync(int id, AddOrEditTradeMarkViewModel model);

        Task<bool> TradeMarkExistsByIdAsync(int? id);
    }
}

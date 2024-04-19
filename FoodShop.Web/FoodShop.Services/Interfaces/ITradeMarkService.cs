using FoodShop.Web.ViewModels.TradeMark;

namespace FoodShop.Services.Interfaces
{
    public interface ITradeMarkService
    {
        Task<ICollection<TradeMarkViewModel>> GetAllTradeMarksAsync();
    }
}

using FoodShop.Data;
using FoodShop.Services.Interfaces;
using FoodShop.Web.ViewModels.TradeMark;
using Microsoft.EntityFrameworkCore;

namespace FoodShop.Services
{
    public class TradeMarkService : ITradeMarkService
    {
        private readonly FoodShopDbContext dbContext;

        public TradeMarkService(FoodShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<ICollection<TradeMarkViewModel>> GetAllTradeMarksAsync()
        {
            ICollection<TradeMarkViewModel> tradeMarks = await this.dbContext
                .TradeMarks
                .Select (t => new TradeMarkViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                }).ToArrayAsync ();

            return tradeMarks;
        }
    }
}

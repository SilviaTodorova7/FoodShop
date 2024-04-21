using FoodShop.Data;
using FoodShop.Data.Models;
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

        public async Task AddTradeMarkAsync(AddOrEditTradeMarkViewModel model)
        {
            TradeMark tradeMark = new TradeMark()
            {
                Name = model.Name,
            };

            await dbContext.TradeMarks.AddAsync(tradeMark);
            await dbContext.SaveChangesAsync();
        }

        public async Task EditTradeMarkAsync(int id, AddOrEditTradeMarkViewModel model)
        {
            TradeMark tradeMark = await this.dbContext
                .TradeMarks
                .FirstAsync(t => t.Id == id);

            tradeMark.Name = model.Name;
            await this.dbContext.SaveChangesAsync();
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

        public async Task<AddOrEditTradeMarkViewModel> GetForEditTradeMarkAsync(int id)
        {
            TradeMark tradeMark = await this.dbContext
                .TradeMarks
                .FirstAsync(t => t.Id == id);

            AddOrEditTradeMarkViewModel model = new AddOrEditTradeMarkViewModel()
            {
                Name = tradeMark.Name,
            };

            return model;
        }

        public async Task<bool> TradeMarkExistsByIdAsync(int? id)
        {
            bool existsById = await this.dbContext.TradeMarks
                .AnyAsync (t => t.Id == id);

            return existsById;
        }
    }
}

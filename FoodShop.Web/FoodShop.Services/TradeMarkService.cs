using FoodShop.Data;
using FoodShop.Data.Models;
using FoodShop.Services.Interfaces;
using FoodShop.Web.ViewModels.Product;
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

        public async Task<ICollection<string>> GetAllTradeMarksNamesAsync()
        {
            return await this.dbContext.TradeMarks.Select (t => t.Name).ToArrayAsync ();
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

        public async Task<ICollection<ProductFromTradeMarkViewModel>> GetProductsFromTradeMarkAsync(int id)
        {
            ICollection<ProductFromTradeMarkViewModel> products = await this.dbContext
                .Products
                .Where(p => p.TradeMarkId == id)
                .Select(p => new ProductFromTradeMarkViewModel()
                {
                    TradeMarkId = id,
                    ProductId = p.Id,
                    ProductName = p.Name,
                    ProductPrice = p.Price,
                    Count = p.Quantity,
                }).ToArrayAsync();

            return products;
        }

        public async Task<TradeMarkViewModel> GetTradeMarkAndProductsAsync(int id)
        {
            TradeMark trademark = await dbContext.TradeMarks.FirstAsync(t => t.Id == id);

            TradeMarkViewModel model = new TradeMarkViewModel()
            {
                Id = id,
                Name = trademark.Name,
                Products = await this.GetProductsFromTradeMarkAsync(id),
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

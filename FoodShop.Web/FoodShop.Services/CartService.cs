using FoodShop.Data;
using FoodShop.Services.Interfaces;
using FoodShop.Web.ViewModels.Product;
using Microsoft.EntityFrameworkCore;

namespace FoodShop.Services
{
    public class CartService : ICartService
    {
        private readonly FoodShopDbContext dbContext;

        public CartService(FoodShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<ICollection<CartProductViewModel>> GoToCart(string userId)
        {
            ICollection<CartProductViewModel> productRowsModel = await this.dbContext
                .UserProducts
                .Where(up => up.UserId.ToString() == userId)
                .Select(up => new CartProductViewModel()
                {
                    ProductId = up.ProductId,
                    ProductName = up.Product.Name,
                    ProductPrice = up.Product.Price,
                    Count = up.Count,
                    UserId = userId,
                })
                .ToArrayAsync();

            return productRowsModel;
        }
    }
}

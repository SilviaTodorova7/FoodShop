using FoodShop.Data;
using FoodShop.Data.Models;
using FoodShop.Services.Interfaces;
using FoodShop.Web.ViewModels.Product;
using Microsoft.EntityFrameworkCore;
using static FoodShop.Common.NotificationMessagesConstants;

namespace FoodShop.Services
{
    public class CartService : ICartService
    {
        private readonly FoodShopDbContext dbContext;

        public CartService(FoodShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> BuyProductsAsync(string userId)
        {
            ICollection<UserProduct> userProductsToBuy = await this.dbContext
                .UserProducts
                .Where(up => up.UserId.ToString() == userId)
                .ToArrayAsync();

            foreach (var up in userProductsToBuy)
            {
                Product productToBuy = await this.dbContext.Products
                    .FirstAsync(p => p.Id == up.ProductId);

                if (productToBuy.Quantity < up.Count)
                {
                    return $"{productToBuy.Quantity} {productToBuy.Name}";
                }
                    productToBuy.Quantity -= up.Count;
            }

            this.dbContext.UserProducts.RemoveRange(userProductsToBuy);
            await this.dbContext.SaveChangesAsync();
            return string.Empty;
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

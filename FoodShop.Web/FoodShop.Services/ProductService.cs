using FoodShop.Data;
using FoodShop.Services.Interfaces;
using FoodShop.Web.ViewModels.Product;
using Microsoft.EntityFrameworkCore;

namespace FoodShop.Services
{
    public class ProductService : IProductService
    {
        private readonly FoodShopDbContext dbContext;
        public ProductService(FoodShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<ICollection<AllProductsViewModel>> GetAllProductsAsync()
        {
            AllProductsViewModel[] model = await this.dbContext.Products.Select(p => new AllProductsViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                Category = p.Category.Name,
                TradeMark = p.TradeMark.Name,
                ProductType = p.ProductType.Name,
                Price = p.Price,
                PictureUrl = p.PictureUrl,
            })
                .ToArrayAsync();

            return model;
        }
    }
}

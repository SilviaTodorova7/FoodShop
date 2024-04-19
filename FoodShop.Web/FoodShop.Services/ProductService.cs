using FoodShop.Data;
using FoodShop.Data.Models;
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

        public async Task<ProductDetailsViewModel> GetProductDetailsAsync(int id)
        {
            Product product = await this.dbContext
                .Products
                .Include(p => p.Category)
                .Include(p => p.TradeMark)
                .Include(p => p.ProductType)
                .FirstAsync(p => p.Id == id);

            ICollection<CommentViewModel> comments = await this.dbContext
                .Comments
                .Where(c => c.ProductId == id)
                .Select(c => new CommentViewModel()
                {
                    Id = c.Id,
                    Title = c.Title,
                    Content = c.Content,
                    User = c.User.UserName,
                })
                .ToArrayAsync();

            ProductDetailsViewModel model = new ProductDetailsViewModel()
            {
                Id = id,
                Name = product.Name,
                Description = product.Description,
                Category = product.Category.Name,
                ProductType = product.ProductType.Name,
                Price = product.Price.ToString(),
                PictureUrl = product.PictureUrl,
            };
            
            if (comments != null)
            {
                model.Comments = comments;
            }

            if (model.TradeMark != null)
            {
                model.TradeMark = product.TradeMark!.ToString();
            }

            return model;
        }
    }
}

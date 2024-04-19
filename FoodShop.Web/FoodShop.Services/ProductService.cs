﻿using FoodShop.Data;
using FoodShop.Data.Models;
using FoodShop.Services.Interfaces;
using FoodShop.Web.ViewModels.Comment;
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
                model.TradeMark = product.TradeMark!.Name;
            }

            return model;
        }

        public async Task<AddOrEditProductViewModel> GetProductForAddOrEditAsync(int id)
        {
            Product product = await this.dbContext
                .Products
                .Include(p => p.TradeMark)
                .Include(p => p.ProductType)
                .Include(p => p.Category)
                .FirstAsync(p => p.Id == id);

            AddOrEditProductViewModel model = new AddOrEditProductViewModel()
            {
                Id = id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price.ToString(),
                PictureUrl = product.PictureUrl,
                Quantity = product.Quantity,
                CategoryId = product.CategoryId,
                ProductTypeId = product.ProductTypeId,
            };

            if (product.TradeMark != null)
            {
                model.TradeMarkId = product.TradeMarkId;
            }

            return model;
        }
    }
}

﻿using FoodShop.Data;
using FoodShop.Data.Models;
using FoodShop.Services.Interfaces;
using FoodShop.Services.Models.Product;
using FoodShop.Web.ViewModels.Comment;
using FoodShop.Web.ViewModels.Product;
using FoodShop.Web.ViewModels.Product.Enums;
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

        public async Task AddProductAsync(AddOrEditProductViewModel model)
        {
            Product product = new Product()
            {
                Name = model.Name,
                Description = model.Description,
                PictureUrl = model.PictureUrl,
                CategoryId = model.CategoryId,
                ProductTypeId = model.ProductTypeId,
                TradeMarkId = model.TradeMarkId,
                Quantity = model.Quantity,
                Price = model.Price,
            };

            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> AddProductToCartAsync(int id, string userId)
        {
            Product productToAdd = await this.dbContext.Products
                .FirstAsync(p => p.Id == id);

            if (productToAdd.Quantity == 0)
            {
                return false;
            }

            UserProduct? userProduct = await this.dbContext
                .UserProducts
                .FirstOrDefaultAsync(up => up.UserId.ToString() == userId && up.ProductId == id);

            if (userProduct == null)
            {
                UserProduct newUserProduct = new UserProduct()
                {
                    ProductId = id,
                    UserId = Guid.Parse(userId),
                    Count = 1,
                };
                
                this.dbContext.UserProducts.Add(newUserProduct);
                this.dbContext.SaveChanges();
            }
            else
            {
                userProduct.Count += 1;
                this.dbContext.SaveChanges();
            }

            return true;
        }

        public async Task<AllProductsFilteredAndPagedServiceModel> AllAsync(AllProductQueryModel queryModel)
        {
            IQueryable<Product> productsQuery = dbContext.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryModel.Category))
            {
                productsQuery = productsQuery.Where(p => p.Category.Name == queryModel.Category);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.ProductType))
            {
                productsQuery = productsQuery.Where(p => p.ProductType.Name == queryModel.ProductType);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.TradeMark))
            {
                productsQuery = productsQuery.Where(p => p.TradeMark.Name == queryModel.TradeMark);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.SearchString))
            {
                string wildCard = $"%{queryModel.SearchString}%";

                productsQuery = productsQuery
                    .Where(p => EF.Functions.Like(p.Name, wildCard) ||
                        EF.Functions.Like(p.Description, wildCard) ||
                        EF.Functions.Like(p.ProductType.Name, wildCard) ||
                        EF.Functions.Like(p.TradeMark.Name, wildCard) ||
                        EF.Functions.Like(p.Category.Name, wildCard));
            }
            productsQuery = queryModel.ProductSorting switch
            {
                ProductSorting.PriceAscending => productsQuery
                .OrderBy(p => p.Price),
                ProductSorting.PriceDescending => productsQuery
                .OrderByDescending(p => p.Price),
                ProductSorting.NameAscending => productsQuery
                .OrderBy(p => p.Name),
                _ => productsQuery
            };

            IEnumerable<AllProductsViewModel> allProducts = await productsQuery
                .Where(p => p.IsActive)
                .Skip((queryModel.CurrentPage - 1) * queryModel.ProductsPerPage)
                .Take(queryModel.ProductsPerPage)
                .Select(p => new AllProductsViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    PictureUrl = p.PictureUrl,
                    Category = p.Category.Name,
                    Price = p.Price,
                    ProductType = p.ProductType.Name,
                    TradeMark = p.TradeMark.Name,
                }).ToArrayAsync();

            int totalProducts = productsQuery.Count();

            return new AllProductsFilteredAndPagedServiceModel()
            {
                TotalProducts = totalProducts,
                Products = allProducts,
            };

        }

        public async Task DeleteProductByIdAsync(int id, DeleteProductViewModel model)
        {
            Product product = await this.dbContext
                .Products
                .Where(p => p.IsActive)
                .FirstAsync(p => p.Id == id);

            product.IsActive = false;
            await this.dbContext.SaveChangesAsync();
        }

        public async Task EditProductAsync(AddOrEditProductViewModel model, int id)
        {
            Product product = await this.dbContext
                .Products
                .Where(p => p.IsActive)
                .Include(p => p.TradeMark)
                .Include(p => p.ProductType)
                .Include(p => p.Category)
                .FirstAsync(p => p.Id == id);

            product.Name = model.Name;
            product.Description = model.Description;
            product.PictureUrl = model.PictureUrl;
            product.CategoryId = model.CategoryId;
            product.ProductTypeId = model.ProductTypeId;
            product.TradeMarkId = model.TradeMarkId;
            product.Quantity = model.Quantity;
            product.Price = model.Price;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistByIdAsync(int id)
        {
            bool existById = await this.dbContext
                .Products
                .Where(p => p.IsActive)
                .AnyAsync(p => p.Id == id);

            return existById;
        }

        public async Task<ProductDetailsViewModel> GetProductDetailsAsync(int id)
        {
            Product product = await this.dbContext
                .Products
                .Where(p => p.IsActive)
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

        public async Task<AddOrEditProductViewModel> GetProductForEditAsync(int id)
        {
            Product product = await this.dbContext
                .Products
                .Where(p => p.IsActive)
                .Include(p => p.TradeMark)
                .Include(p => p.ProductType)
                .Include(p => p.Category)
                .FirstAsync(p => p.Id == id);

            AddOrEditProductViewModel model = new AddOrEditProductViewModel()
            {
                Id = id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
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

        public async Task<DeleteProductViewModel> GetProductViewDetailsToDeleteAsync(int id)
        {
            Product product = await this.dbContext
                .Products
                .Where(p => p.IsActive)
                .FirstAsync(p => p.Id == id);

            DeleteProductViewModel model = new DeleteProductViewModel()
            {
                Name = product.Name,
                PictureUrl = product.PictureUrl,
                Price = product.Price,
            };

            if (product.TradeMark != null)
            {
                model.TradeMark = product.TradeMark.Name;
            };

            return model;
        }

        public async Task<bool> ProductExistByName(string name)
        {
            bool existByName = await this.dbContext
                .Products
                .Where(p => p.IsActive)
                .AnyAsync(p => p.Name == name);

            return existByName;
        }

        public async Task RemoveProductFromCartAsync(int id, string userId)
        {
            UserProduct userProductToRemove = await this.dbContext
                .UserProducts
                .FirstAsync(up => up.UserId.ToString() == userId && up.ProductId == id);

            if (userProductToRemove.Count > 1)
            {
                userProductToRemove.Count -= 1;

                this.dbContext.SaveChanges();
            }
            else if (userProductToRemove.Count == 1)
            {
                this.dbContext.UserProducts.Remove(userProductToRemove);
                this.dbContext.SaveChanges(true);
            }
        }
    }
}

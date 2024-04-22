using FoodShop.Data;
using FoodShop.Data.Models;
using FoodShop.Services.Interfaces;
using FoodShop.Web.ViewModels.Product;
using FoodShop.Web.ViewModels.ProductType;
using Microsoft.EntityFrameworkCore;

namespace FoodShop.Services
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly FoodShopDbContext dbContext;
        public ProductTypeService(FoodShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddProductTypeAsync(AddOrEditProductTypeViewModel model)
        {
            ProductType productType = new ProductType()
            {
                Name = model.Name,
            };

            await this.dbContext.ProductTypes.AddAsync(productType);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task EditProductTypeAsync(int id, AddOrEditProductTypeViewModel model)
        {
            ProductType productType = await this.dbContext.ProductTypes
               .FirstAsync(pt => pt.Id == id);

            productType.Name = model.Name;
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<ProductTypeViewModel>> GetAllProductTypesAsync()
        {
            ICollection<ProductTypeViewModel> productTypes = await this.dbContext
                .ProductTypes
                .Select(pt => new ProductTypeViewModel()
                {
                    Id = pt.Id,
                    Name = pt.Name,
                }).ToArrayAsync();

            return productTypes;
        }

        public async Task<ICollection<string>> GetAllProductTypesNamesAsync()
        {
            return await this.dbContext.ProductTypes.Select(pt => pt.Name).ToArrayAsync();
        }

        public async Task<AddOrEditProductTypeViewModel> GetProductTypeForEditAsync(int id)
        {
           ProductType productType = await this.dbContext.ProductTypes
                .FirstAsync(pt => pt.Id == id);

            AddOrEditProductTypeViewModel viewModel = new AddOrEditProductTypeViewModel()
            {
                Name = productType.Name,
            };

            return viewModel;
        }

        public async Task<bool> ProductTypeExistsByIdAsync(int id)
        {
            bool existsById = await this.dbContext.ProductTypes
                .AnyAsync(pt => pt.Id == id);

            return existsById;
        }
    }
}

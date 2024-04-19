using FoodShop.Data;
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
    }
}

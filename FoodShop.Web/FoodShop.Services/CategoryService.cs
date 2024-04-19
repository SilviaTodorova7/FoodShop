using FoodShop.Data;
using FoodShop.Services.Interfaces;
using FoodShop.Web.ViewModels.Category;
using Microsoft.EntityFrameworkCore;

namespace FoodShop.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly FoodShopDbContext dbContext;
        public CategoryService(FoodShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<ICollection<CategoryViewModel>> GetAllCategoriesAsync()
        {
            ICollection<CategoryViewModel> categories = await this.dbContext
                .Categories
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToArrayAsync();

            return categories;
        }
    }
}

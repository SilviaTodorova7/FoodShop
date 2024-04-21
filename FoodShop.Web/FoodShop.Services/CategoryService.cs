using FoodShop.Data;
using FoodShop.Data.Models;
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

        public async Task AddCategoryAsync(AddOrEditCategoryViewModel model)
        {
            Category category = new Category()
            {
                Name = model.Name,
            };

            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> CategoryExistsByIdAsync(int id)
        {
            bool existsById = await this.dbContext.Categories
                .AnyAsync(c => c.Id == id);

            return existsById;
        }

        public async Task EditCategoryAsync(int id, AddOrEditCategoryViewModel model)
        {
            Category category = await this.dbContext.Categories
                .FirstAsync(c => c.Id == id);

            category.Name = model.Name;
            await dbContext.SaveChangesAsync();
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

        public async Task<AddOrEditCategoryViewModel> GetCategoryForEditAsync(int id)
        {
            Category category = await this.dbContext.Categories
                .FirstAsync(c => c.Id == id);

            AddOrEditCategoryViewModel viewModel = new AddOrEditCategoryViewModel()
            {
                Name = category.Name,
            };

            return viewModel;
        }
    }
}

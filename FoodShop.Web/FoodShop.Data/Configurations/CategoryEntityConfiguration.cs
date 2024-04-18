using FoodShop.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodShop.Data.Configurations
{
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(this.GenerateCategories());
        }

        private Category[] GenerateCategories()
        {
            ICollection<Category> categories = new HashSet<Category>();

            Category category;

            category = new Category
            {
                Id = 1,
                Name = "Milk Products"
            };
            categories.Add(category);

            category = new Category
            {
                Id = 2,
                Name = "Bread"
            };
            categories.Add(category);

            category = new Category
            {
                Id = 3,
                Name = "Meat"
            };
            categories.Add(category);

            category = new Category
            {
                Id = 4,
                Name = "Fruits"
            };
            categories.Add(category);

            category = new Category
            {
                Id = 5,
                Name = "Vegetables"
            };
            categories.Add(category);

            category = new Category
            {
                Id = 6,
                Name = "Eggs"
            };
            categories.Add(category);

            return categories.ToArray();
        }

    }
}

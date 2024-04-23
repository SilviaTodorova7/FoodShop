using FoodShop.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodShop.Data.Configurations
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.IsActive)
                .HasDefaultValue(true);

            builder.HasOne(p => p.TradeMark)
                   .WithMany(t => t.Products)
                   .HasForeignKey(p => p.TradeMarkId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Category)
                   .WithMany(c => c.Products)
                   .HasForeignKey(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.ProductType)
                   .WithMany(t => t.Products)
                   .HasForeignKey(p => p.ProductTypeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.Price)
                   .HasPrecision(17, 2);

            builder.HasData(this.AddProducts());
        }

        private Product[] AddProducts()
        {
            ICollection<Product> products = new HashSet<Product>();

            Product product;

            product = new Product()
            {
                Id = 1,
                Name = "Bread",
                Description = "White sliced bread, prepared from wheat flour.",
                CategoryId = 2,
                ProductTypeId = 1,
                TradeMarkId = 3,
                PictureUrl = "https://cdncloudcart.com/16398/products/images/50027/hlab-mio-bal-narazan-650-gr-image_63bfea2451a4c_600x600.png?1673521707",
                Price = 1.60m,
                Quantity = 35,
            };
            products.Add(product);

            product = new Product()
            {
                Id = 2,
                Name = "Milk",
                Description = "Cow milk",
                CategoryId = 1,
                ProductTypeId = 1,
                TradeMarkId = 1,
                PictureUrl = "https://pingo.bg/Content/products/124/800-800.jpg",
                Price = 3.70m,
                Quantity = 30,
            };
            products.Add(product);

            product = new Product()
            {
                Id = 3,
                Name = "Ham",
                Description = "Pork Ham",
                CategoryId = 3,
                ProductTypeId = 1,
                PictureUrl = "https://www.wenthere8this.com/wp-content/uploads/2021/10/sous-vide-ham-6.jpg",
                Price = 8.90m,
                Quantity = 20,
            };
            products.Add(product);

            product = new Product()
            {
                Id = 4,
                Name = "Apples",
                Description = "Green Apples",
                CategoryId = 4,
                ProductTypeId = 2,
                PictureUrl = "https://letsgetittraining.com/wp-content/uploads/2019/01/Green-Apples-1.jpg",
                Price = 3.90m,
                Quantity = 40,
            };
            products.Add(product);

            product = new Product()
            {
                Id = 5,
                Name = "Tomatoes",
                Description = "Tomatoes",
                CategoryId = 5,
                ProductTypeId = 3,
                PictureUrl = "https://www.liveeatlearn.com/wp-content/uploads/2019/05/common-types-of-tomatoes-20.jpg",
                Price = 2.90m,
                Quantity = 60,
            };
            products.Add(product);

            product = new Product()
            {
                Id = 6,
                Name = "Eggs",
                Description = "Organic Eggs",
                CategoryId = 6,
                ProductTypeId = 2,
                TradeMarkId = 6,
                PictureUrl = "https://www.ecowatch.com/wp-content/uploads/2021/09/1893442151-img.jpg",
                Price = 6.90m,
                Quantity = 20,
            };
            products.Add(product);

            return products.ToArray();
        }
    }
}

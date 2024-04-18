using FoodShop.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodShop.Data.Configurations
{
    public class ProductTypeEntityConfiguration : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(EntityTypeBuilder<ProductType> builder)
        {
            builder.HasData(this.GenerateProductTypes());
        }

        private ProductType[] GenerateProductTypes()
        {
            ICollection<ProductType> productTypes = new HashSet<ProductType>();

            ProductType productType;

            productType = new ProductType()
            {
                Id = 1,
                Name = "New",
            };
            productTypes.Add(productType);

            productType = new ProductType()
            {
                Id = 2,
                Name = "Organic",
            };
            productTypes.Add(productType);

            productType = new ProductType()
            {
                Id = 3,
                Name = "Promo",
            };
            productTypes.Add(productType);

            return productTypes.ToArray();
        }
    }
}

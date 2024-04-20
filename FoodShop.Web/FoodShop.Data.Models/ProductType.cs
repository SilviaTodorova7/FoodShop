using System.ComponentModel.DataAnnotations;
using static FoodShop.Common.EntityValidationConstants.ProductType;

namespace FoodShop.Data.Models
{
    public class ProductType
    {
        public ProductType()
        {
            this.Products = new HashSet<Product>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public ICollection<Product> Products { get; set; }
    }
}

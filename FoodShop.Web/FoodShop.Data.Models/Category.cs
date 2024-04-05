using System.ComponentModel.DataAnnotations;
using static FoodShop.Common.EntityValidationConstants.Category;

namespace FoodShop.Data.Models
{
    public class Category
    {
        public Category()
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

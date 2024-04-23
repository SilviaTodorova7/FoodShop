using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodShop.Data.Models
{
    public class CartItem
    {
        public CartItem()
        {
            this.Count = 0;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        public Product Product { get; set; } = null!;

        public int Count { get; set; }
    }
}

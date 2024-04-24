using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodShop.Data.Models
{
    public class Cart
    {
        public Cart()
        {
            this.CartItems = new HashSet<CartItem>();
            this.Totalprice = 0;
        }

        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; } = null!;

        public ICollection<CartItem> CartItems { get; set; }

        public decimal Totalprice { get; set; }
    }
}

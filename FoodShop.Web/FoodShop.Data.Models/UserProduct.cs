using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FoodShop.Data.Models
{
    public class UserProduct
    {
        public UserProduct()
        {

        }

        [ForeignKey(nameof(ApplicationUser))]
        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; } = null!;

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        public Product Product { get; set; } = null!;

        public int Count { get; set; }
    }
}

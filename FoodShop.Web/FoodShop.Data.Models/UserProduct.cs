using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodShop.Data.Models
{
    public class UserProduct
    {
        public UserProduct()
        {
            this.Date = DateTime.UtcNow.Date; 
        }

        [ForeignKey(nameof(ApplicationUser))]
        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; } = null!;

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        public Product Product { get; set; } = null!;

        public int Count { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}

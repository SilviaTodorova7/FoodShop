using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodShop.Data.Models
{
    public class Order
    {
        public Order()
        {
            this.Date = DateTime.UtcNow;
        }

        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Cart))]
        public int CartId { get; set; }

        public Cart Cart { get; set; } = null!;

        public DateTime Date { get; set; }
    }
}

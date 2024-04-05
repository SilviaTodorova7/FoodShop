using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static FoodShop.Common.EntityValidationConstants.Product;

namespace FoodShop.Data.Models
{
    public class Product
    {
        public Product()
        {
            this.Comments = new HashSet<Comment>();
            this.UserProducts = new HashSet<UserProduct>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public Category Category { get; set; } = null!;

        [ForeignKey(nameof(Type))]
        public int ProductTypeId { get; set; }

        public ProductType ProductType { get; set; } = null!;

        [ForeignKey(nameof(TradeMark))]
        public int TradeMarkId { get; set; }

        public TradeMark TradeMark { get; set; } = null!;

        [Required]
        [MaxLength(PictureUrlMaxLength)]
        public string PictureUrl { get; set; } = null!;

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<UserProduct> UserProducts { get; set; }
    }
}

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
        public int? TradeMarkId { get; set; }

        public TradeMark? TradeMark { get; set; }

        [Required]
        [MaxLength(PictureUrlMaxLength)]
        public string PictureUrl { get; set; } = null!;

        public decimal Price { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        public bool IsActive { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}

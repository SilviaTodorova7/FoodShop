using System.ComponentModel.DataAnnotations;

namespace FoodShop.Web.ViewModels.Product
{
    public class AllProductsViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string ProductType { get; set; } = null!;
        public string? TradeMark { get; set; }
        public string PictureUrl { get; set; } = null!;
        public decimal Price { get; set; }
    }
}

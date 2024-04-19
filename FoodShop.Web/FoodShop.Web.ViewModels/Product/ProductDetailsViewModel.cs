using FoodShop.Data.Models;

namespace FoodShop.Web.ViewModels.Product
{
    public class ProductDetailsViewModel
    {
        public ProductDetailsViewModel()
        {
            this.Comments = new HashSet<CommentViewModel>();
        }
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Category { get; set; } = null!;

        public string ProductType { get; set; } = null!;

        public string? TradeMark { get; set; }

        public string PictureUrl { get; set; } = null!;

        public string Price { get; set; } = null!;

        public ICollection<CommentViewModel> Comments { get; set; }
    }
}

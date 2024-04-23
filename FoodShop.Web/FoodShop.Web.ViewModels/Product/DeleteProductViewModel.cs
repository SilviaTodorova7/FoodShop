namespace FoodShop.Web.ViewModels.Product
{
    public class DeleteProductViewModel
    {
        public string Name { get; set; } = null!;
        public string? TradeMark { get; set; }
        public string PictureUrl { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
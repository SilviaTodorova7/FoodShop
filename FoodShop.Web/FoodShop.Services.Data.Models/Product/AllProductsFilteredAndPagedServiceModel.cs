using FoodShop.Web.ViewModels.Product;

namespace FoodShop.Services.Models.Product
{
    public class AllProductsFilteredAndPagedServiceModel
    {
        public AllProductsFilteredAndPagedServiceModel()
        {
            this.Products = new HashSet<AllProductsViewModel>();
        }
        public int TotalProducts { get; set; }

        public IEnumerable<AllProductsViewModel> Products { get; set; }
    }
}

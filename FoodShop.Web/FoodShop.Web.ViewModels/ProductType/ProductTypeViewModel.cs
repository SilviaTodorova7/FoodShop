using FoodShop.Web.ViewModels.Product;
using System.ComponentModel.DataAnnotations;

namespace FoodShop.Web.ViewModels.ProductType
{
    public class ProductTypeViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public ICollection<ProductFromProductTypeViewModel> Products { get; set; } = null!;
    }
}

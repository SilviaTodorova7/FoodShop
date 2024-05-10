using FoodShop.Web.ViewModels.Product;
using System.ComponentModel.DataAnnotations;

namespace FoodShop.Web.ViewModels.Category
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public ICollection<ProductFromCategoryViewModel> Products { get; set; } = null!;

    }
}

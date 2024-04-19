using System.ComponentModel.DataAnnotations;

namespace FoodShop.Web.ViewModels.ProductType
{
    public class ProductTypeViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }
}

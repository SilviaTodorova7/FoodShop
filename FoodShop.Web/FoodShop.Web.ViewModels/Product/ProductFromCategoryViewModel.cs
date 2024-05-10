using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShop.Web.ViewModels.Product
{
    public class ProductFromCategoryViewModel
    {
        public int CategoryId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public int Count { get; set; }

        public decimal ProductPrice { get; set; }
    }
}

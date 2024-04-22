using FoodShop.Web.ViewModels.Category;
using FoodShop.Web.ViewModels.Product.Enums;
using FoodShop.Web.ViewModels.ProductType;
using FoodShop.Web.ViewModels.TradeMark;
using System.ComponentModel.DataAnnotations;
using static FoodShop.Common.GeneralApplicationConstants;

namespace FoodShop.Web.ViewModels.Product
{
    public class AllProductQueryModel
    {
        public AllProductQueryModel()
        {
            this.Categories = new HashSet<string>();
            this.ProductTypes = new HashSet<string>();
            this.TradeMarks = new HashSet<string>();
            this.Products = new HashSet<AllProductsViewModel>();
            this.CurrentPage = DefaultCurrentPage;
            this.ProductsPerPage = DefaultProductsPerPage;
        }
        public string? Category { get; set; }

        public string? ProductType { get; set; }

        public string? TradeMark { get; set; }

        [Display(Name = "Search by word")]
        public string? SearchString { get; set; }

        [Display(Name = "Sort Products By")]
        public ProductSorting ProductSorting { get; set; }

        public int CurrentPage { get; set; }

        public int ProductsPerPage { get; set; }

        public int TotalProducts { get; set; }

        public IEnumerable<string> Categories { get; set; }

        public IEnumerable<string> ProductTypes { get; set; }

        public IEnumerable<string> TradeMarks { get; set; }

        public IEnumerable<AllProductsViewModel> Products { get; set; }
    }
}

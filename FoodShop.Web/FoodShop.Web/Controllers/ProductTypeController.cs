using FoodShop.Services.Interfaces;
using FoodShop.Web.ViewModels.ProductType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace FoodShop.Web.Controllers
{
    [Authorize]
    public class ProductTypeController : Controller
    {
        private readonly IProductTypeService productTypeService;
        public ProductTypeController(IProductTypeService productTypeService)
        {
            this.productTypeService = productTypeService;
        }
        public async Task<IActionResult> All()
        {
            ICollection<ProductTypeViewModel> model = await this.productTypeService
                .GetAllProductTypesAsync();

            return View(model);
        }
    }
}

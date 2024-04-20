using FoodShop.Services;
using FoodShop.Services.Interfaces;
using FoodShop.Web.ViewModels.TradeMark;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodShop.Web.Controllers
{
    [Authorize]
    public class TradeMarkController : Controller
    {
        private readonly ITradeMarkService tradeMarkService;
        public TradeMarkController(ITradeMarkService tradeMarkService)
        {
            this.tradeMarkService = tradeMarkService;
        }
        public async Task<IActionResult> All()
        {
            ICollection<TradeMarkViewModel> model = await this.tradeMarkService
                .GetAllTradeMarksAsync();

            return View(model);
        }
    }
}

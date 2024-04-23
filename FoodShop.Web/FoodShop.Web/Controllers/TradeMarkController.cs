using FoodShop.Data.Models;
using FoodShop.Services.Interfaces;
using FoodShop.Web.ViewModels.TradeMark;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FoodShop.Common.NotificationMessagesConstants;

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

        [HttpGet]
        public async Task<IActionResult> All()
        {
            ICollection<TradeMarkViewModel> model = await this.tradeMarkService
                .GetAllTradeMarksAsync();

            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            AddOrEditTradeMarkViewModel model = new AddOrEditTradeMarkViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddOrEditTradeMarkViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await this.tradeMarkService.AddTradeMarkAsync(model);
                this.TempData[SuccessMessage] = "You have added new Trademark successfully!";
                return RedirectToAction("All", "TradeMark");
            }
            catch (Exception)
            {
                ModelState.AddModelError(nameof(TradeMark), "Something went wrong while adding Trademark. Please try again later or contact administrator!");
                return RedirectToAction("All", "TradeMark");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            AddOrEditTradeMarkViewModel model = await this.tradeMarkService
                .GetForEditTradeMarkAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddOrEditTradeMarkViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await this.tradeMarkService.EditTradeMarkAsync(id, model);
                this.TempData[SuccessMessage] = "You have edited Trademark successfully!";

                return RedirectToAction("All", "TradeMark");
            }
            catch (Exception)
            {
                ModelState.AddModelError(nameof(TradeMark), "Something went wrong while editing Trademark. Please try again later or contact administrator!");
                return View(model);
            }
        }
    }
}

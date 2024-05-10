using FoodShop.Data.Models;
using FoodShop.Services.Interfaces;
using FoodShop.Web.Infrastructure.Extensions;
using FoodShop.Web.ViewModels.Product;
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
            if (User.IsAdmin())
            {
                AddOrEditTradeMarkViewModel model = new AddOrEditTradeMarkViewModel();

                return View(model);
            }
            else
            {
                this.TempData[WarningMessage] = "You have to be administrator to reach this page!";
                
                return RedirectToAction("All", "Product");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddOrEditTradeMarkViewModel model)
        {
            if (User.IsAdmin())
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
            else
            {
                this.TempData[WarningMessage] = "You have to be administrator to reach this page!";
                
                return RedirectToAction("All", "Product");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (User.IsAdmin())
            {
                try
                {
                    AddOrEditTradeMarkViewModel model = await this.tradeMarkService
                        .GetForEditTradeMarkAsync(id);

                    return View(model);
                }
                catch (Exception)
                {
                    ModelState.AddModelError(nameof(TradeMark), "Something went wrong while editing Trademark. Please try again later or contact administrator!");
                    return RedirectToAction("All", "TradeMark");
                }
            }
            else
            {
                this.TempData[WarningMessage] = "You have to be administrator to reach this page!";
                
                return RedirectToAction("All", "Product");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddOrEditTradeMarkViewModel model)
        {
            if (User.IsAdmin())
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
            else
            {
                this.TempData[WarningMessage] = "You have to be administrator to reach this page!";

                return RedirectToAction("All", "Product");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ViewProducts(int id)
        {
            if (User.IsAdmin())
            {
                try
                {
                    TradeMarkViewModel model = await this.tradeMarkService
                        .GetTradeMarkAndProductsAsync(id);

                    return View(model);
                }
                catch (Exception)
                {
                    ModelState.AddModelError(nameof(TradeMark), "Something went wrong while trying to view products from Trademark. Please try again later or contact administrator!");
                    this.TempData[ErrorMessage] = "Something went wrong while trying to view products from Trademark. Please try again later or contact administrator!";

                    return RedirectToAction("All", "TradeMark");
                }
            }
            else
            {
                this.TempData[WarningMessage] = "You have to be administrator to reach this page!";
                
                return RedirectToAction("All", "Product");
            }
        }
    }
}

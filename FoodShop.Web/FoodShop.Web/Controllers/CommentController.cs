using FoodShop.Data.Models;
using FoodShop.Services.Interfaces;
using FoodShop.Web.ViewModels.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static FoodShop.Web.Infrastructure.Extensions.ClaimsPrincipalExtensions;

namespace FoodShop.Web.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ICommentService commentService;
        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        [HttpGet]
        public IActionResult Add(int id)
        {

            AddCommentViewModel model = new AddCommentViewModel()
            {
                ProductId = id,
                UserName = User.Identity!.Name!,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCommentViewModel model)
        {
            model.UserName = User.Identity!.Name!;
            if (!ModelState.IsValid)
            {
               return View(model);
            }

            try
            {
                model.UserName = User.Identity!.Name!;
                string userId = User.GetUserId()!;
                await this.commentService.AddNewCommentAsync(model, model.ProductId, userId);
                return RedirectToAction("Details", "Product", new {id = model.ProductId});

            }
            catch (Exception)
            {
                ModelState.AddModelError(nameof(model.Content), "Something went wrong while adding your comment. Please try again later or contact administrator!");
                return View(model);
            }
        }
    }
}

using FoodShop.Web.ViewModels.Comment;

namespace FoodShop.Services.Interfaces
{
    public interface ICommentService
    {
        Task AddNewCommentAsync(AddCommentViewModel model, int productId, string uerId);
    }
}

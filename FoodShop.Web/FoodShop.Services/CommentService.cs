using FoodShop.Data;
using FoodShop.Data.Models;
using FoodShop.Services.Interfaces;
using FoodShop.Web.ViewModels.Comment;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static FoodShop.Web.Infrastructure.Extensions.ClaimsPrincipalExtensions;

namespace FoodShop.Services
{
    public class CommentService : ICommentService
    {
        private readonly FoodShopDbContext dbContext;
        public CommentService(FoodShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task AddNewCommentAsync(AddCommentViewModel model, int productId, string userId)
        {
            ApplicationUser user = await this.dbContext.Users.FirstAsync(u => u.Id == Guid.Parse(userId));
            Product product = await this.dbContext.Products.FirstAsync(p => p.Id == productId);

            Comment comment = new Comment()
            {
                Title = model.Title,
                Content = model.Content,
                ProductId = productId,
                UserId = Guid.Parse(userId),
                User = user,
                Product = product,
            };

            await this.dbContext.Comments.AddAsync(comment);
            await this.dbContext.SaveChangesAsync();
        }
    }
}

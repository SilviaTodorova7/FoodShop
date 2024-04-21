using FoodShop.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static FoodShop.Common.EntityValidationConstants.Comment;
using System.ComponentModel;

namespace FoodShop.Web.ViewModels.Comment
{
    public class AddCommentViewModel
    {
        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength, ErrorMessage = "Comment title must be between {2} and {1} characters long.")]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(ContentMaxLength, MinimumLength = ContentMinLength, ErrorMessage = "Comment must be between {2} and {1} characters long.")]
        public string Content { get; set; } = null!;

        public string UserName { get; set; } = null!;

        [Range(0, int.MaxValue)]
        public int ProductId { get; set; }
    }
}

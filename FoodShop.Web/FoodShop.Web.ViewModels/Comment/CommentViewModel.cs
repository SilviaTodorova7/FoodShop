namespace FoodShop.Web.ViewModels.Comment
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Content { get; set; } = null!;

        public string User { get; set; } = null!;

    }
}

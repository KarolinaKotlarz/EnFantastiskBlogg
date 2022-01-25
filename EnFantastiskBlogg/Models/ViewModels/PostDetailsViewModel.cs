namespace EnFantastiskBlogg.Models.ViewModels
{
    public class PostDetailsViewModel
    {
        public int PostId { get; set; }
        public Post Post { get; set; }
        public CreateCommentViewModel Comment { get; set; }
    }
}

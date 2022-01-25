namespace EnFantastiskBlogg.Models.ViewModels
{
    public class CreateCommentViewModel
    {
        public int PostId { get; set; }
        public Post Post { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

    }
}

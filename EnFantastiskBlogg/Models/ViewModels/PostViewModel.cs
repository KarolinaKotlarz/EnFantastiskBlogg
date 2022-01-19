namespace EnFantastiskBlogg.Models.ViewModels
{
    public class PostViewModel
    {
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Body { get; set; }
        public ApplicationUser User { get; set; }
    }
}

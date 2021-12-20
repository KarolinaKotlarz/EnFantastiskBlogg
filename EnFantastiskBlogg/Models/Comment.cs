using Microsoft.AspNetCore.Identity;

namespace EnFantastiskBlogg.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public IdentityUser User { get; set; }

    }
}

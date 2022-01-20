using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EnFantastiskBlogg.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Body { get; set; }
        public List<Comment> Comments { get; set;} = new List<Comment>();
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
    }
}

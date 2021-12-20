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
        public List<Post> Posts { get; set;}
        public IdentityUser User { get; set; }
        public int UserId { get; set; }
    }
}

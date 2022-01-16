using Microsoft.AspNetCore.Identity;

namespace EnFantastiskBlogg.Models
{
    public class ApplicationUser : IdentityUser<string>
    {
        public int PostCount { get; set; }
    }
}

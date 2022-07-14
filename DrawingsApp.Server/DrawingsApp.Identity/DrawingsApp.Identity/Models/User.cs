using Microsoft.AspNetCore.Identity;

namespace DrawingsApp.Identity.Models
{
    public class User:IdentityUser
    {
        public string? AvatarUrl { get; set; }
    }
}

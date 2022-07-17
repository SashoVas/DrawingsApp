using Microsoft.AspNetCore.Identity;

namespace DrawingsApp.Identity.Data.Models
{
    public class User:IdentityUser
    {
        public string? AvatarUrl { get; set; }
    }
}

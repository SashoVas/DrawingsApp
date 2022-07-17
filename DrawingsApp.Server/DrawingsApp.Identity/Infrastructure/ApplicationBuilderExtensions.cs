using DrawingsApp.Identity.Data;
using DrawingsApp.Identity.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace DrawingsApp.Identity.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static void AddIdentity(this WebApplicationBuilder builder)
        {
            builder.Services.AddIdentity<User, IdentityRole>(options => {
                options.SignIn.RequireConfirmedEmail = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 1;
            })
            .AddEntityFrameworkStores<DrawingsAppIdentityDbContext>()
            .AddDefaultTokenProviders();
        }
    }
}

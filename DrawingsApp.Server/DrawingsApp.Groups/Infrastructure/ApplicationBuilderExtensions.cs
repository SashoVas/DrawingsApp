using DrawingsApp.Groups.Services;
using DrawingsApp.Groups.Services.Contracts;

namespace DrawingsApp.Groups.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IGroupService, GroupService>();
            builder.Services.AddTransient<ITagService, TagService>();
            builder.Services.AddTransient<IPostService, PostService>();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IAsynchronousDbOperationsService, AsynchronousDbOperationsService>();
        }
    }
}

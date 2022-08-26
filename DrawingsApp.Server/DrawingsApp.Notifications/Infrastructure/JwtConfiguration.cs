using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Primitives;

namespace DrawingsApp.Notifications.Infrastructure
{
    public class JwtConfiguration
    {
        public static JwtBearerEvents BearerEvents
            => new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    if (context.Request.Query.TryGetValue("token", out StringValues token)
                    )
                    {
                        context.Token = token;
                    }

                    return Task.CompletedTask;
                },
                OnAuthenticationFailed = context =>
                {
                    var te = context.Exception;
                    return Task.CompletedTask;
                }
            };
    }
}

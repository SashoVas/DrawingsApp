using Microsoft.Net.Http.Headers;

namespace DrawingsApp.Images.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static void AddStaticFiles(this WebApplication app)
        {
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = preperation =>
                {
                    var headers = preperation.Context.Response.GetTypedHeaders();
                    headers.CacheControl = new CacheControlHeaderValue
                    {
                        Public = true,
                        MaxAge = TimeSpan.FromDays(1)
                    };
                    headers.Expires = new DateTimeOffset(DateTime.UtcNow.AddDays(1));
                    preperation.Context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                }
            });
        }
    }
}

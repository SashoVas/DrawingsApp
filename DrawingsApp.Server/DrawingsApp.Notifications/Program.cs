using DrawingsApp.Infrastructure;
using DrawingsApp.Notifications.Hubs;
using DrawingsApp.Notifications.Messages.Post;
using DrawingsApp.Notifications.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.AddCors();
builder.AddAuthenticationWithJWT(JwtConfiguration.BearerEvents);
builder.AddMessages(typeof(PostCreatedMessageConsumer));
builder.Services.AddSignalR();
var app = builder.Build();
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapHub<NotificationsHub>("/Notifications");
app.Run();
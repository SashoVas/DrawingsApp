using DrawingsApp.Infrastructure;
using DrawingsApp.Notifications.Hubs;
using DrawingsApp.Notifications.Messages.Post;
using DrawingsApp.Notifications.Infrastructure;
using DrawingsApp.Notifications.Data;
using DrawingsApp.Notifications.Messages.User;

var builder = WebApplication.CreateBuilder(args);
builder.AddCors();
builder.AddAuthenticationWithJWT(JwtConfiguration.BearerEvents);
builder.AddMessages(
    typeof(PostCreatedMessageConsumer),
    typeof(EnableNotificationsMessageConsumer));
builder.Services.AddSingleton<MongoDbNotificationsRepository>();
builder.Services.AddSignalR();
var app = builder.Build();
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapHub<NotificationsHub>("/Notifications");
app.Run();
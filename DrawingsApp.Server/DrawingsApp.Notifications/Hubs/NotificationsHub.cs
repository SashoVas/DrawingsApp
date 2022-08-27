using DrawingsApp.Notifications.Data;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace DrawingsApp.Notifications.Hubs
{
    public class NotificationsHub : Hub
    {
        private readonly MongoDbNotificationsRepository repo;

        public NotificationsHub(MongoDbNotificationsRepository repo)
        {
            this.repo = repo;
        }

        public async override Task OnConnectedAsync()
        {
            if (this.Context.User.Identity.IsAuthenticated)
            {
                var a=(await repo.GetUsersNotifications(this.Context.User.FindFirstValue(ClaimTypes.NameIdentifier)));
                foreach (var notification in a)
                {
                    await this.Groups.AddToGroupAsync(
                    this.Context.ConnectionId,
                    notification.GroupId.ToString());
                }
            }
        }
        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            if (this.Context.User.Identity.IsAuthenticated)
            {
                var a = (await repo.GetUsersNotifications(this.Context.User.FindFirstValue(ClaimTypes.NameIdentifier)));
                foreach (var notification in a)
                {
                    await this.Groups.RemoveFromGroupAsync(
                    this.Context.ConnectionId,
                    notification.GroupId.ToString());
                }
            }
        }
    }
}

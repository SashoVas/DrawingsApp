using Microsoft.AspNetCore.SignalR;

namespace DrawingsApp.Notifications.Hubs
{
    public class NotificationsHub : Hub
    {
        public async override Task OnConnectedAsync()
        {
            if (this.Context.User.Identity.IsAuthenticated)
            {
                await this.Groups.AddToGroupAsync(
                    this.Context.ConnectionId,
                    "Group");
            }
        }
        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            if (this.Context.User.Identity.IsAuthenticated)
            {
                await this.Groups.RemoveFromGroupAsync(
                    this.Context.ConnectionId,
                    "Group");
            }
        }
    }
}

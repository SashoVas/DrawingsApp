using DrawingsApp.Messages.Post;
using DrawingsApp.Notifications.Hubs;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace DrawingsApp.Notifications.Messages.Post
{
    public class PostCreatedMessageConsumer : IConsumer<PostCreatedMessage>
    {
        private readonly IHubContext<NotificationsHub> hub;

        public PostCreatedMessageConsumer(IHubContext<NotificationsHub> hub)
        {
            this.hub = hub;
        }

        public async Task Consume(ConsumeContext<PostCreatedMessage> context)
        {
            await hub.Clients.Group("Group").SendAsync("ShowNotification",context.Message);
        }
    }
}

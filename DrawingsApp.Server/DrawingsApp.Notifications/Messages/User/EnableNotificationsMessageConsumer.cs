using DrawingsApp.Messages.User;
using DrawingsApp.Notifications.Data;
using MassTransit;

namespace DrawingsApp.Notifications.Messages.User
{
    public class EnableNotificationsMessageConsumer : IConsumer<EnableNotificationsMessage>
    {
        private readonly MongoDbNotificationsRepository repo;
        public EnableNotificationsMessageConsumer(MongoDbNotificationsRepository repo)
        {
            this.repo = repo;
        }

        public async Task Consume(ConsumeContext<EnableNotificationsMessage> context)
        {
            var ug =await repo.Find(context.Message.UserId,context.Message.GroupId);
            if (ug is null)
            {
                await repo.Insert(new Data.Models.Notification 
                {
                    UserId= context.Message.UserId ,
                    GroupId= context.Message.GroupId
                });
            }
            else
            {
                await repo.Delete(context.Message.UserId, context.Message.GroupId);
            }
        }
    }
}

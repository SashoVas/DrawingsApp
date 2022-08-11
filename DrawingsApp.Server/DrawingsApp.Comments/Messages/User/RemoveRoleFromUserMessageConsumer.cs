using DrawingsApp.Comments.Data.Repositories;
using DrawingsApp.Messages.User;
using MassTransit;

namespace DrawingsApp.Comments.Messages.User
{
    public class RemoveRoleFromUserMessageConsumer : IConsumer<RemoveRoleFromUserMessage>
    {
        private readonly IUserRoleInGroupRepository repo;

        public RemoveRoleFromUserMessageConsumer(IUserRoleInGroupRepository repo) => this.repo = repo;

        public async Task Consume(ConsumeContext<RemoveRoleFromUserMessage> context) 
            => await repo.RemoveOne(context.Message.UserId, context.Message.GroupId);
    }
}

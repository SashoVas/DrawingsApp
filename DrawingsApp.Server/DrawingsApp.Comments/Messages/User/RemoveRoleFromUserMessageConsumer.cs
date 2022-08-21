using DrawingsApp.Comments.Services.Contracts;
using DrawingsApp.Messages.User;
using MassTransit;

namespace DrawingsApp.Comments.Messages.User
{
    public class RemoveRoleFromUserMessageConsumer : IConsumer<RemoveRoleFromUserMessage>
    {
        private readonly IGroupService groupService;

        public RemoveRoleFromUserMessageConsumer(IGroupService groupService) => this.groupService = groupService;

        public async Task Consume(ConsumeContext<RemoveRoleFromUserMessage> context) 
            => await groupService.RemoveRole( context.Message.GroupId, context.Message.UserId);
    }
}

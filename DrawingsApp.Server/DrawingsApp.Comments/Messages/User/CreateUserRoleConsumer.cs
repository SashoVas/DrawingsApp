using DrawingsApp.Comments.Services.Contracts;
using DrawingsApp.Messages.User;
using MassTransit;

namespace DrawingsApp.Comments.Messages.User
{
    public class CreateUserRoleConsumer : IConsumer<CreateUserRoleMessage>
    {
        private readonly IGroupService groupService;

        public CreateUserRoleConsumer(IGroupService groupService)
        {
            this.groupService = groupService;
        }

        public async Task Consume(ConsumeContext<CreateUserRoleMessage> context)
        {
            await groupService.AddUserToGroup(context.Message.GroupId,
                context.Message.UserId,
                context.Message.Role);
        }
    }
}

using DrawingsApp.Comments.Services.Contracts;
using DrawingsApp.Messages.User;
using MassTransit;

namespace DrawingsApp.Comments.Messages.User
{
    public class PromoteUserRoleInGroupConsumer : IConsumer<PromoteUserRoleInGroupMessage>
    {
        private readonly IGroupService groupService;

        public PromoteUserRoleInGroupConsumer(IGroupService groupService) => this.groupService = groupService;

        public async Task Consume(ConsumeContext<PromoteUserRoleInGroupMessage> context)
        {
            await groupService.UpdateRole(context.Message.GroupId,
                context.Message.UserId,
                context.Message.Role);
        }
    }
}

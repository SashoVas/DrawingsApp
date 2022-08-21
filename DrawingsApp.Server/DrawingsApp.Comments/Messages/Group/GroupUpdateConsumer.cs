using DrawingsApp.Comments.Services.Contracts;
using DrawingsApp.Messages.Group;
using MassTransit;

namespace DrawingsApp.Comments.Messages.Group
{
    public class GroupUpdateConsumer : IConsumer<GroupUpdateMessage>
    {
        private readonly IGroupService groupService;

        public GroupUpdateConsumer(IGroupService groupService)
        {
            this.groupService = groupService;
        }

        public async Task Consume(ConsumeContext<GroupUpdateMessage> context)
        {
            await groupService.UpdateGroup(context.Message.GroupId,
                context.Message.GroupName,
                context.Message.GroupType);
        }
    }
}

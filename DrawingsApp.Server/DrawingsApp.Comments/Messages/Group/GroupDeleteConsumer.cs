using DrawingsApp.Comments.Services.Contracts;
using DrawingsApp.Messages.Group;
using MassTransit;

namespace DrawingsApp.Comments.Messages.Group
{
    public class GroupDeleteConsumer : IConsumer<GroupDeleteMessage>
    {
        private readonly IGroupService groupService;

        public GroupDeleteConsumer(IGroupService groupService)
        {
            this.groupService = groupService;
        }

        public async Task Consume(ConsumeContext<GroupDeleteMessage> context)
        {
            await groupService.DeleteGroup(context.Message.GroupId);
        }
    }
}

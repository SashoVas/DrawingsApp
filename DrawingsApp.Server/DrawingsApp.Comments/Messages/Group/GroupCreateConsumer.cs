using DrawingsApp.Comments.Data.Models;
using DrawingsApp.Comments.Services.Contracts;
using DrawingsApp.Messages.Group;
using MassTransit;

namespace DrawingsApp.Comments.Messages.Group
{
    public class GroupCreateConsumer : IConsumer<GroupCreateMessage>
    {
        private readonly IGroupService groupService;
        public GroupCreateConsumer(IGroupService groupService)
        {
            this.groupService = groupService;
        }
        public async Task Consume(ConsumeContext<GroupCreateMessage> context)
        {
            await groupService.CreateGroup(context.Message.GroupId,
                context.Message.GroupName,
                context.Message.GroupType,
                context.Message.UserId);
        }
    }
}

using DrawingsApp.Comments.Data.Repositories;
using DrawingsApp.Messages.Group;
using MassTransit;

namespace DrawingsApp.Comments.Messages.Group
{
    public class GroupDeleteConsumer : IConsumer<GroupDeleteMessage>
    {
        private readonly IGroupRepository groupRepo;

        public GroupDeleteConsumer(IGroupRepository groupRepo)
        {
            this.groupRepo = groupRepo;
        }

        public async Task Consume(ConsumeContext<GroupDeleteMessage> context)
        {
            await groupRepo.DeleteGroup(context.Message.GroupId);
        }
    }
}

using DrawingsApp.Comments.Data.Repositories;
using DrawingsApp.Messages.Group;
using MassTransit;

namespace DrawingsApp.Comments.Messages.Group
{
    public class GroupUpdateConsumer : IConsumer<GroupUpdateMessage>
    {
        private readonly IGroupRepository groupRepo;

        public GroupUpdateConsumer(IGroupRepository groupRepo)
        {
            this.groupRepo = groupRepo;
        }

        public async Task Consume(ConsumeContext<GroupUpdateMessage> context)
        {
            await groupRepo.UpdateGroup(new Data.Models.GroupInfo 
            {
                GroupId=context.Message.GroupId,
                GroupName=context.Message.GroupName,
                GroupType=context.Message.GroupType
            });
        }
    }
}

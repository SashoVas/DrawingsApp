using DrawingsApp.Comments.Data.Models;
using DrawingsApp.Comments.Data.Repositories;
using DrawingsApp.Messages.Group;
using MassTransit;

namespace DrawingsApp.Comments.Messages.Group
{
    public class GroupCreateConsumer : IConsumer<GroupCreateMessage>
    {
        private readonly IGroupRepository groupRepo;
        private readonly IUserRoleInGroupRepository userRoleInGroupRepo;

        public GroupCreateConsumer(IGroupRepository groupRepo, IUserRoleInGroupRepository userRoleInGroupRepo)
        {
            this.groupRepo = groupRepo;
            this.userRoleInGroupRepo = userRoleInGroupRepo;
        }

        public async Task Consume(ConsumeContext<GroupCreateMessage> context)
        {
            await groupRepo.CreateGroup(new GroupInfo 
            {
                GroupId=context.Message.GroupId,
                GroupName=context.Message.GroupName,
                GroupType=context.Message.GroupType
            });

            await userRoleInGroupRepo.UpdateRole(new UserRoleInGroup
            {
                GroupId=context.Message.GroupId,
                Role=DrawingsApp.Data.Common.Role.Admin,
                UserId=context.Message.UserId
            });
        }
    }
}

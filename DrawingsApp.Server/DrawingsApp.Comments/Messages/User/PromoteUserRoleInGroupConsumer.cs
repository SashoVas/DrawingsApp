using DrawingsApp.Comments.Data.Models;
using DrawingsApp.Comments.Data.Repositories;
using DrawingsApp.Messages.User;
using MassTransit;

namespace DrawingsApp.Comments.Messages.User
{
    public class PromoteUserRoleInGroupConsumer : IConsumer<PromoteUserRoleInGroupMessage>
    {
        private readonly IUserRoleInGroupRepository repo;

        public PromoteUserRoleInGroupConsumer(IUserRoleInGroupRepository repo) => this.repo = repo;

        public async Task Consume(ConsumeContext<PromoteUserRoleInGroupMessage> context)
        {
            await repo.UpdateRole(new UserRoleInGroup
            {
                Role = context.Message.Role,
                GroupId = context.Message.GroupId,
                UserId = context.Message.UserId
            });
        }
    }
}

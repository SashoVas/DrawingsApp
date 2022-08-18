using DrawingsApp.Groups.Services.Contracts;
using DrawingsApp.Messages.Post;
using MassTransit;

namespace DrawingsApp.Groups.Messages.Post
{
    public class PostCreatedConsumer : IConsumer<PostCreatedMessage>
    {
        private readonly IPostService postService;

        public PostCreatedConsumer(IPostService postService)
        {
            this.postService = postService;
        }

        public async Task Consume(ConsumeContext<PostCreatedMessage> context)
        {
            await postService.CreatePost(context.Message.SenderId,context.Message.Title,context.Message.GroupId,context.Message.Images,context.Message.Id);
        }
    }
}

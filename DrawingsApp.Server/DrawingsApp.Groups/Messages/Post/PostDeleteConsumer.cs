using DrawingsApp.Groups.Services.Contracts;
using DrawingsApp.Messages.Post;
using MassTransit;

namespace DrawingsApp.Groups.Messages.Post
{
    public class PostDeleteConsumer : IConsumer<PostDeleteMessage>
    {
        private readonly IPostService postService;

        public PostDeleteConsumer(IPostService postService)
        {
            this.postService = postService;
        }

        public async Task Consume(ConsumeContext<PostDeleteMessage> context)
        {
            await postService.DeletePost(context.Message.Id);
        }
    }
}

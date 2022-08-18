using DrawingsApp.Comments.Services.Contracts;
using DrawingsApp.Messages.Post;
using MassTransit;

namespace DrawingsApp.Comments.Messages.Post
{
    public class LikePostConsumer : IConsumer<PostLikeMessage>
    {
        private readonly IPostService postService;

        public LikePostConsumer(IPostService postService)
        {
            this.postService = postService;
        }

        public async Task Consume(ConsumeContext<PostLikeMessage> context)
        {
            await postService.LikePost(context.Message.PostId,context.Message.ChangeAmounth);
        }
    }
}

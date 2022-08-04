using DrawingsApp.Comments.Services.Contracts;
using DrawingsApp.Messages.Post;
using MassTransit;

namespace DrawingsApp.Comments.Messages
{
    public class PostDeletedConsumer : IConsumer<PostDeleteMessage>
    {
        private readonly IPostService postService;

        public PostDeletedConsumer(IPostService postService) 
            => this.postService = postService;

        public async Task Consume(ConsumeContext<PostDeleteMessage> context) 
            => await postService.DeletePost(context.Message.Id);
    }
}

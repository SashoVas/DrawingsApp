using DrawingsApp.Comments.Data.Models;
using DrawingsApp.Comments.Services.Contracts;
using DrawingsApp.Messages.Post;
using MassTransit;

namespace DrawingsApp.Comments.Messages.Post
{
    public class PostCreatedConsumer : IConsumer<PostCreatedMessage>
    {
        private readonly IPostService postService;

        public PostCreatedConsumer(IPostService postService) 
            => this.postService = postService;

        public async Task Consume(ConsumeContext<PostCreatedMessage> context) 
            => await postService.CreatePost(
                context.Message.Id,
                context.Message.GroupId,
                context.Message.GroupName,
                context.Message.Title,
                context.Message.Description,
                context.Message.SenderId,
                context.Message.SenderName,
                context.Message.Images,
                (PostType)context.Message.PostType);
    }
}

﻿using DrawingsApp.Groups.Services.Contracts;
using DrawingsApp.Messages.Post;
using MassTransit;

namespace DrawingsApp.Groups.Messages.Post
{
    public class PostUpdatedConsumer : IConsumer<PostUpdateMessage>
    {
        private readonly IPostService postService;

        public PostUpdatedConsumer(IPostService postService)
        {
            this.postService = postService;
        }

        public async Task Consume(ConsumeContext<PostUpdateMessage> context)
        {
            await postService.UpdatePost(context.Message.Id,context.Message.Title);
        }
    }
}

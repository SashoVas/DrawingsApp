﻿using DrawingsApp.Comments.Data.Models;

namespace DrawingsApp.Comments.Data.Repositories
{
    public interface IPostRepository
    {
        Task<Post> GetPost(string postId);
        Task<IEnumerable<Post>> GetPosts();
        Task CreatePost(Post post);
        Task UpdatePost(Post post);
        Task DeletePosts();
    }
}

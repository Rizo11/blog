using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Entities;
using api.Models;

namespace api.Services
{
    public interface IPostService
    {
        Task<(bool IsSuccess, Exception Exception, Post Post)> CreatePostAsync(Post nPost);
        Task<bool> ExistsPostAsync(Guid id);
        Task<Post> GetPostAsync(Guid id);
        Task<List<Post>> GetAllPostsAsync();
        Task<(bool IsSuccess, Exception Exception)> DeletePostAsync(Guid id);
        Task<(bool IsSuccess, Exception Exception, Post post)> UpdatePostAsync(Post post);

    }
}
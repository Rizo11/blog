using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Entities;
using api.Models;

namespace api.Services
{
    public interface ICommentService
    {
        Task<(bool IsSuccess, Exception Exception, Comment Comment)> CreateCommentAsync(NewComment comment);
        Task<List<Comment>> GetAllCommentsAsync();
        Task<Comment> GetCommentAsync(Guid id);
        Task<(bool IsSuccess, Exception Exception)> DeleteCommentAsync(Guid id);
        Task<bool> ExistsCommentAsync(Guid id);
    }
}
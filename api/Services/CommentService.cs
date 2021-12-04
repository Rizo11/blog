using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data;
using api.Entities;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace api.Services
{
    public class CommentService : ICommentService
    {
        private BlogContext _ctx;
        private ILogger<CommentService> _logger;

        public CommentService(BlogContext context, ILogger<CommentService> logger)
        {
            _ctx = context;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, Exception Exception, Comment Comment)> CreateCommentAsync(NewComment comment)
        {
            try
            {
                var commentEntity = comment.toCommentEntity();
                await _ctx.Comments.AddAsync(commentEntity);
                await _ctx.SaveChangesAsync();
                _logger.LogInformation($"new comment added {comment}");
                return(true, null, commentEntity);
            }
            catch(Exception e)
            {
                _logger.LogError($"Failed to add new comment");
                return(false, e, null);
            }
        }

        public async Task<(bool IsSuccess, Exception Exception)> DeleteCommentAsync(Guid id)
        {
            var com = await GetCommentAsync(id);
            if(com == default(Comment))
            {
                return (false, new ArgumentException("Comment NOT FOUND!"));
            }

            try
            {
                _ctx.Comments.Remove(com);
                await _ctx.SaveChangesAsync();

                return (true, null);
            }
            catch(Exception e)
            {
                return(false, e);
            }
        }

        public Task<bool> ExistsCommentAsync(Guid id)
            => _ctx.Comments.AnyAsync(x => x.Id == id);

        public Task<List<Comment>> GetAllCommentsAsync()
            => _ctx.Comments.ToListAsync();

        public Task<Comment> GetCommentAsync(Guid id)
            => _ctx.Comments.FirstOrDefaultAsync(x => x.Id == id);
    }
}
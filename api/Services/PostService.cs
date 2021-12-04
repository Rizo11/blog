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
    public class PostService : IPostService
    {
        private readonly ILogger<PostService> _logger;
        private readonly IMediaService _mservice;
        private readonly BlogContext _context;

        public PostService(ILogger<PostService> logger, BlogContext context, IMediaService mediaService)
        {
            _logger = logger;
            _mservice = mediaService;
            _context = context;
        }


        public async Task<(bool IsSuccess, Exception Exception, Post Post)> CreatePostAsync(Post Post)
        {
            // var postEntity = nPost.toPostEntity();
            if(!await _mservice.ExistsMediaAsync(Post.HeaderImageId))
            {
                return(false, new Exception("Header image not found!"), null);
            }
            try
            {
                await _context.AddAsync(Post);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"New post created: {Post.Id}");

                return(true, null, Post);
            }
            catch(Exception e)
            {
                _logger.LogError($"Failed to add new post");
                return(false, e, null);
            }
        }

        public async Task<(bool IsSuccess, Exception Exception)> DeletePostAsync(Guid id)
        {
            var post = await GetPostAsync(id);
            if(post == default(Post))
            {
                return (false, new Exception("Post Not found"));
            }
            try
            {
                _context.Posts.Remove(post);
                foreach(var media in post.Medias)
                {
                    _context.Medias.Remove(media);
                }
                foreach(var comment in post.Coments)
                {
                    _context.Comments.Remove(comment);
                }

                await _context.SaveChangesAsync();

                _logger.LogInformation($"Post deleted : {post.Id}");

                return(true, null);
            }
            catch(Exception e)
            {
                _logger.LogError($"Failed to delete {post.Id}");
                return(false, e);
            }
        }

        public Task<bool> ExistsPostAsync(Guid id)
            => _context.Posts.AnyAsync(x => x.Id == id);

        public Task<List<Post>> GetAllPostsAsync()
            => _context.Posts.Include(p => p.Medias).Include(p => p.Coments).ToListAsync();

        public Task<Post> GetPostAsync(Guid id)
            => _context.Posts.Include(p => p.Coments).Include(p => p.Medias).FirstOrDefaultAsync(p => p.Id == id);

        public async Task<(bool IsSuccess, Exception Exception, Post post)> UpdatePostAsync(Post post)
        {
            try
            {
                _context.Posts.Update(post);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Post updated: {post.Id}");
                return(true, null, post);
            }
            catch(Exception e)
            {
                _logger.LogError($"Failed to update: {post.Id}");
                return(false, e, post);
            }
        }
    }
}
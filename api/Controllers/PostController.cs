using System.Net;
using System.Reflection.Metadata.Ecma335;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Entities;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostCotroller : ControllerBase
    {
        private readonly IPostService _post;
        private readonly IMediaService _media;
        private readonly BlogContext _context;
        private readonly ILogger<PostCotroller> _logger;

        public PostCotroller(IPostService postService, IMediaService mediaService, BlogContext context, ILogger<PostCotroller> logger)
        {
            _post = postService;
            _media = mediaService;
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(NewPost post)
        {
            ICollection<Media> medias = new List<Media>();
            foreach (var item in post.Medias)
            {
                if(! await _media.ExistsMediaAsync(item))
                {

                    return BadRequest($"Medias with ID: {item} not found");
                } 
                medias.Add(await _media.GetMediaAsync(item));
            }
            var postEntity = new Post()
            {
                Id = Guid.NewGuid(),
                HeaderImageId = post.HeaderImageId,
                Title = post.Title,
                Description = post.Description,
                Content = post.Content,
                Vieved = 0,
                CreatedAt = DateTimeOffset.UtcNow,
                ModifiedAt = DateTimeOffset.UtcNow,
                Coments = null,
                Medias = medias
            };

            var result = await _post.CreatePostAsync(postEntity);
            if(result.IsSuccess)
            {
                _logger.LogInformation($"N");
                return Ok(new{
                Id = result.Post.Id,
                HeaderImageId = result.Post.HeaderImageId,
                Title = result.Post.Title,
                Content = result.Post.Content,
                Viewed = result.Post.Vieved,
                CreatedAt = result.Post.CreatedAt,
                ModifiedAt = result.Post.ModifiedAt,
                Comments = result.Post.Coments.Select(c => new
                {
                    Id = c.Id,
                    Author = c.Author,
                    Content = c.Content,
                    State = c.State,
                    PostId = c.PostId
                }),
                Medias = result.Post.Medias.Select(x => new
                {
                    Id = x.Id,
                    ContentType = x.ContentType
                })
            });
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var posts = await _post.GetAllPostsAsync();
            var json = posts.Select(p => new
            {
                Id = p.Id,
                HeaderImageId = p.HeaderImageId,
                Title = p.Title,
                Content = p.Content,
                Viewed = p.Vieved,
                CreatedAt = p.CreatedAt,
                ModifiedAt = p.ModifiedAt,
                Comments = p.Coments.Select(c => new
                {
                    Id = c.Id,
                    Author = c.Author,
                    Content = c.Content,
                    State = c.State,
                    PostId = c.PostId
                }),
                Medias = p.Medias.Select(x => new
                {
                    Id = x.Id,
                    ContentType = x.ContentType
                })
            });
            return Ok(json);
        }

        [HttpGet]
        [Route("{id}")]     
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var post = await _post.GetPostAsync(id);
            return Ok(
                new{
                Id = post.Id,
                HeaderImageId = post.HeaderImageId,
                Title = post.Title,
                Content = post.Content,
                Viewed = post.Vieved,
                CreatedAt = post.CreatedAt,
                ModifiedAt = post.ModifiedAt,
                Comments = post.Coments.Select(c => new
                {
                    Id = c.Id,
                    Author = c.Author,
                    Content = c.Content,
                    State = c.State,
                    PostId = c.PostId
                }),
                Medias = post.Medias.Select(x => new
                {
                    Id = x.Id,
                    ContentType = x.ContentType
                })
            }
            );
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> PutAsync([FromRoute] Guid id, NewPost newPost)
        {
            ICollection<Media> medias = new List<Media>();
            if(!await _post.ExistsPostAsync(id))
            {
                return BadRequest($"Post with id: {id} not found");
            }
            foreach (var item in newPost.Medias)
            {
                medias.Add(await _media.GetMediaAsync(item));
            }
            var postEntity = new Post()
            {
                Id = id,
                HeaderImageId = newPost.HeaderImageId,
                Title = newPost.Title,
                Description = newPost.Description,
                Content = newPost.Content,
                Coments = null,
                Medias = medias
            };

            var result = await _post.UpdatePostAsync(postEntity);
            if(result.IsSuccess)
            {
                return Ok(result.post);
            }
            return BadRequest("Failed to update");
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            return Ok(await _post.DeletePostAsync(id));
        }

    }
}
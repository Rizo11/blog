using System;
using System.Linq;
using System.Threading.Tasks;
using api.Entities;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController: ControllerBase
    {
        private readonly ICommentService _service;

        public CommentController( ICommentService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetComment([FromRoute]Guid id)
        {
            var result =await _service.GetCommentAsync(id);
            if(result == default(Comment))
            {
                return BadRequest("No such a comment");
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetMedias()
        {
            var commnets = await _service.GetAllCommentsAsync();
            var result = commnets.Select(u => new {
                Id = u.Id,
                Author = u.Author,
                Content = u.Content,
                State = u.State,
                postId = u.PostId
            });

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostMedia([FromForm]Models.NewComment newComment)
        {
            var result = await _service.CreateCommentAsync(newComment);
            if(!result.IsSuccess)
            {
                return BadRequest(result.Exception.Message);
            }

            return Ok(result.Comment);
        }
    }
}
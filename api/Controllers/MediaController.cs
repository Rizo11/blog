using System.Net.Mime;
using System.IO;
using System;
using System.Threading.Tasks;
using api.Data;
using api.Entities;
using api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MediaController: ControllerBase
    {
        private readonly IMediaService _service;

        public MediaController( IMediaService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetMedia([FromRoute]Guid id)
        {
            var result =await _service.GetMediaAsync(id);
            if(result == default(Media))
            {
                return BadRequest("No such a media");
            }

            var stream = new MemoryStream(result.Data);
            return File(stream, result.ContentType);
        }

        [HttpGet]
        public async Task<IActionResult> GetMedias()
        {
            var medias = await _service.GetAllMediasAsync();
            var result = medias.Select(u => new {
                Id = u.Id,
                ContentType = u.ContentType,
            });

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostMedia([FromForm]Models.NewMedia newMedias)
        {
            var result = await _service.CreateMediaAsync(newMedias);
            if(!result.IsSuccess)
            {
                return BadRequest(result.Exception.Message);
            }

            return Ok(result.Media.Select(x => 
            new {
                Id = x.Id,
                ContentType = x.ContentType,
                Size = $"{x.Data.Length} bytes",
            }));

        }
    }

}
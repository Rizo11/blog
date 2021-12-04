using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Entities;
using api.Models;
using api.Mappers;
using Microsoft.Extensions.Logging;
using api.Data;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class MediaService : IMediaService
    {
        private readonly ILogger<MediaService> _logger;
        private readonly BlogContext _context;

        public MediaService(BlogContext context, ILogger<MediaService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<(bool IsSuccess, Exception Exception, List<Media> Media)> CreateMediaAsync(NewMedia nMedias)

        {
            try
            {
                var mEntitiyes = nMedias.Image.Select(m => m.toMediaEntity()).ToList();
                mEntitiyes.ForEach(m => _context.Medias.AddAsync(m));
                await _context.SaveChangesAsync();
                _logger.LogInformation($"{mEntitiyes.Count} media/s added");
                return(true, null, mEntitiyes);
            }
            catch(Exception e)
            {
                _logger.LogInformation($"Failed to add {nMedias.Image.Count} medias");
                return(false, e, null);
            }
        }

        public async Task<(bool IsSuccess, Exception Exception)> DeleteMediaAsync(Guid id)
        {
            try
            {
                var media = await GetMediaAsync(id);

                if(media == default(Media))
                {
                    return(false, new Exception("Media Not Found"));
                }

                _context.Medias.Remove(media);

                await _context.SaveChangesAsync();
                _logger.LogInformation($"Media {id} was deleted");
                return (true, null);
            }
            catch(Exception e)
            {
                _logger.LogInformation($"Failed to delete media {id}");
                return (false, e);
            }
        }

        public Task<bool> ExistsMediaAsync(Guid id)
            => _context.Medias.AnyAsync(z => z.Id == id);

        public Task<List<Media>> GetAllMediasAsync()
            => _context.Medias.ToListAsync();

        public Task<Media> GetMediaAsync(Guid id)
            => _context.Medias.FirstOrDefaultAsync(x => x.Id == id);
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Entities;
using api.Models;

namespace api.Services
{
    public interface IMediaService
    {
        Task<(bool IsSuccess, Exception Exception, List<Media> Media)> CreateMediaAsync(NewMedia nMedia);
        Task<bool> ExistsMediaAsync(Guid id);
        Task<Media> GetMediaAsync(Guid id);
        Task<List<Media>> GetAllMediasAsync();
        Task<(bool IsSuccess, Exception Exception)> DeleteMediaAsync(Guid id);

    }
}
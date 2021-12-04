using System.IO;
using System.Net.Mime;
using System;
using Microsoft.AspNetCore.Http;

namespace api.Mappers
{
    public static class ToMediaEntity
    {
        public static Entities.Media toMediaEntity(this IFormFile newImage)
        {
            using var stream = new MemoryStream();
            newImage.CopyTo(stream);
            return new Entities.Media()
            {
                Id = Guid.NewGuid(),
                ContentType = newImage.ContentType,
                Data = stream.ToArray()
            };
        }
    }
}
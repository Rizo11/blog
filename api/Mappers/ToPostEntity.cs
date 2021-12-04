using System.Linq;
using System.IO;
using System.Net.Mime;
using System;
using Microsoft.AspNetCore.Http;
using api.Models;

namespace api.Mappers
{
    public static class ToPostEntity
    {
        public static Entities.Post toPostEntity(this NewPost newPost)
        {
            return new Entities.Post()
            {
                Id = Guid.NewGuid(),
                Title = newPost.Title,
                Description = newPost.Description,
                Content = newPost.Content,
                CreatedAt = DateTimeOffset.UtcNow,
                Medias = null,
                Coments = null
            };
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using api.Entities;

namespace api.Models
{
    public class NewPost
    {
        public Guid HeaderImageId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public string Content { get; set; }

        public IEnumerable<Guid> Medias { get; set; }
    }
}
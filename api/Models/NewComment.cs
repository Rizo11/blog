using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Entities;

namespace api.Models
{
    public class NewComment
    {
        [Required]
        [MaxLength(255)]
        public string Author { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public StateEnum State { get; set; }

        [Required]
        public Guid PostId { get; set; }
    }
}
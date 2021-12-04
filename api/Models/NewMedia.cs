using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace api.Models
{
    public class NewMedia
    {
    //     [Required]
    //     public string ContentType { get; set; }
        [Required]
        [MaxLength(1024*1024*3)]
        public ICollection<IFormFile> Image { get; set; }
    }
}
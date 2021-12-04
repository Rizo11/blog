using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;

namespace api.Entities
{
    public class Media
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [MaxLength(55)]
        public string ContentType { get; set; }

        [MaxLength(1024*1024*3)]        
        public byte[] Data { get; set; }
    }
}
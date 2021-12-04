using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;

namespace api.Entities
{
    public class Comment
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Author { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public StateEnum State { get; set; }

        [Required]
        public Guid PostId { get; set; }

        // internal object toCommentEntity()
        // {
        //     throw new NotImplementedException();
        // }
        // public Comment(string author, string content, StateEnum state, Guid postId)
        // {
        //     Id = Guid.NewGuid();
        //     Author = author;
        //     Content = content;
        //     State = state;
        //     PostId = postId;
        // }
    }
}
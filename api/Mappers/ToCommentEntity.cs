using System;
namespace api.Mappers
{
    public static class ToCommentEntity
    {
        public static Entities.Comment toCommentEntity(this Models.NewComment newComment)
        {
            return new Entities.Comment()
            {
                Id = Guid.NewGuid(),
                Author = newComment.Author,
                Content = newComment.Content,
                State = newComment.State,
                PostId = newComment.PostId
            };
        }
    }
}
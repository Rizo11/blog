using api.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class BlogContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Post> Posts { get; set; }


        public BlogContext(DbContextOptions options)
            : base(options) { }
    }
}
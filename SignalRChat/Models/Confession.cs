using System.Collections.Generic;
using System.Data.Entity;

namespace SignalRChat.Models
{
    public class Confession
    {
        public int Id { get; set; }
        public string TheConfession { get; set; }
        public string Submitter { get; set; }
        public int Rank { get; set; }
        public List<Comment> Comments { get; set; }
    }

    public class Comment
    {
        public int Id { get; set; }
        public string TheComment { get; set; }
        public string Submitter { get; set; }
        public int Rank { get; set; }
        public int ConfessionId { get; set; }
    }

    public class ConfessionDbContext : DbContext
    {
        public DbSet<Confession> Confessions { get; set; }
    }

    public class CommentDbContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }

        public System.Data.Entity.DbSet<SignalRChat.Models.Confession> Confessions { get; set; }
    }
}
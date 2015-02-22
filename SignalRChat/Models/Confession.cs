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

        // Chanted/Added after initial creation
        //public List<Comment> Comments { get; set; }

        public virtual ICollection<HashTag> HashTags { get; set; }
    }

    public class HashTag
    {
        // This whole class has changed.

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

    public class HashTagDbContext : DbContext
    {
        public DbSet<HashTag> HashTags { get; set; }

        public System.Data.Entity.DbSet<SignalRChat.Models.Confession> Confessions { get; set; }
    }
}
using System.Collections.Generic;
using System.Data.Entity;

namespace Old.SignalRChat.Models
{
    public class Confession
    {
        public int Id { get; set; }
        public string TheConfession { get; set; }
        public string Submitter { get; set; }
        public int Rank { get; set; }

        public virtual ICollection<HashTag> HashTags { get; set; }

        public Confession()
        {
            this.HashTags = new HashSet<HashTag>();
        }
    }

    public class HashTag
    {

        public int Id { get; set; }

        public virtual ICollection<Confession> Confessions { get; set; }

        public HashTag()
        {
            this.Confessions = new HashSet<Confession>();
        }

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
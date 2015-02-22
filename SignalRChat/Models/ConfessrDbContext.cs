using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SignalRChat.Models
{
    // Pretty sure this is what should be used
    public class ConfessrDbContext : DbContext
    {
        public DbSet<HashTag> HashTags { get; set; }
        public DbSet<Confession> Confessions { get; set; }

        public ConfessrDbContext(string connectionstring) : base(connectionstring) { }
    }


    // And not these...
    public class HashTagDbContext : DbContext
    {
        public DbSet<HashTag> HashTags { get; set; }

        //public System.Data.Entity.DbSet<SignalRChat.Models.Confession> Confessions { get; set; }
    }

    public class ConfessionDbContext : DbContext
    {
        public DbSet<Confession> Confessions { get; set; }

    }
}
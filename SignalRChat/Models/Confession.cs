using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SignalRChat.Models
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


}
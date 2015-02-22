using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SignalRChat.Models
{
    public class HashTag
    {

        public int Id { get; set; }

        public string Tag { get; set; }

        public virtual ICollection<Confession> Confessions { get; set; }

        public HashTag()
        {
            this.Confessions = new HashSet<Confession>();
        }

    }


}
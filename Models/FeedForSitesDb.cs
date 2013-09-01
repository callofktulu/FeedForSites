using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FeedForSites.Models
{
    public class FeedForSitesDb : DbContext
    {
        /*
        public FeedForSitesDb()
            : base("FeedForSitesDb")
        {
        }
         * */
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<FitFit> FitFits { get; set; }

        public DbSet<PrivateMessage> PrivateMessages { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
    }
}
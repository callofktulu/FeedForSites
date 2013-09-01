using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FeedForSites.Models
{
    public class Friendship
    {
        [Key]
        public int FriendshipId { get; set; }

        public int FriendOneId { get; set; }

        [ForeignKey("FriendOneId")]
        public virtual UserProfile FriendOne { get; set; }

        public int FriendTwoId { get; set; }

        [ForeignKey("FriendTwoId")]
        public virtual UserProfile FriendTwo { get; set; }

        public bool Approval { get; set; }

        public DateTime Date { get; set; }
    }
}
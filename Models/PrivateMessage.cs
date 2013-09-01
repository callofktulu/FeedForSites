using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FeedForSites.Models
{
    public class PrivateMessage
    {
        [Key]
        public int PrivateMessageId { get; set; }

        public string Text { get; set; }
        public DateTime Date { get; set; }
        public bool IsRead { get; set; }

        public int SenderUserId { get; set; }
        [ForeignKey("SenderUserId")]
        public virtual UserProfile SenderUser { get; set; }

        public int ReceiverUserId { get; set; }
        [ForeignKey("ReceiverUserId")]
        public virtual UserProfile ReceiverUser { get; set; }

        public string ConversationId { get; set; }
    }

    public class PrivateMessageViewModel
    {
        public int SenderUserId { get; set; }
        public virtual UserProfile SenderUser { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
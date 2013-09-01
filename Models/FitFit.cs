using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FeedForSites.Models
{
    public class FitFit
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual UserProfile User { get; set; }

    }
}
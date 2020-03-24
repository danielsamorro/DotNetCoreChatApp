using ChatApp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Models
{
    public class Message : IEntity
    {
        public Message()
        {
            CreatedOn = DateTime.UtcNow;
        }

        public int MessageID { get; set; }
        [Required]
        public string SentFrom { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ChatUserID { get; set; }
        public virtual ChatUser ChatUser { get; set; }
    }
}

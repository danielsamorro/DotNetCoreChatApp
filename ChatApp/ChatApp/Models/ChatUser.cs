using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Models
{
    public class ChatUser : IdentityUser
    {
        public ChatUser()
        {
            Messages = new List<Message>();
        }

        public IEnumerable<Message> Messages { get; set; }
    }
}

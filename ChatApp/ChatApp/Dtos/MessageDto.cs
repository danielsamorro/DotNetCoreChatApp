using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Dtos
{
    public class MessageDto
    {
        public int MessageID { get; set; }
        public string SentFrom { get; set; }
        public string Text { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}

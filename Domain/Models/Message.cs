using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebHome.Domain.Models
{
    public class Message
    {
        public int id { get; set; }
        public int from_user_id { get; set; }
        public int to_user_id { get; set; }
        public string message_body { get; set; }
        public int apartment_id { get; set; }
    }
}

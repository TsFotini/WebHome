using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebHome.Domain.Models
{
    public class SendMessage
    {
        public string from_username { get; set; }
        public string images { get; set; }
    }
    public class ReceiverMessage : SendMessage
    {
        public int to_user_id { get; set; }
        public int apartment_id { get; set; }
        public string image_apartment { get; set; }

    }
    public class Message : ReceiverMessage
    {
        public int from_user_id { get; set; }
        public int id { get; set; }
        public string message_body { get; set; }
        public string seen { get; set; }


    }

    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebHome.Domain.Models
{
    public class User
    {
        public string user_id { get; set; }
        public string role_id { get; set; }
        public string usrname { get; set; }
        public string email { get; set; }
        public string accepted { get; set; }
        public string last_login { get; set; }
    }

    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebHome.Models
{
    public class Login
    {
        public string usrname { get; set; }
        public string pass { get; set; }
        public int user_id { get; set; }
        public int role_id { get; set; }
    }
}

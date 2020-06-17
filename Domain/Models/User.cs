using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebHome.Domain.Models
{
    public class User
    {
        public int user_id { get; set; }
        public string role { get; set; }
        public string usrname { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string images { get; set; }
        public DateTime created_on { get; set; }
        public DateTime last_login { get; set; }
        public int accepted { get; set; }

    }
}

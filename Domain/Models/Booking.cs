using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebHome.Domain.Models
{
    public class Booking
    {
        public int id { get; set; }
        public int closed { get; set; }
        public DateTime reserved_from { get; set; }
        public DateTime reserved_to { get; set; }
        public int apartment_id { get; set; }
        public int from_user_id { get; set; }
    }

    public class BookHost : Booking
    {
        public string image_apartment { get; set; }
        public string image_user { get; set; }
        public string from_username { get; set; }
    }
}

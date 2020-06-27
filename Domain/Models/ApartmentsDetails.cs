using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebHome.Domain.Models
{
    public class ApartmentsDetails
    {
        public int user_id { get; set; }
        public int max_people { get; set; }
        public string type_description { get; set; }
        public int type_id { get; set; }
        public string description { get; set; }
        public int area { get; set; }
        public string rules { get; set; }
        public float min_price { get; set; }
        public string reach_place { get; set; }
        public float cost_per_person { get; set; }
        public int num_bedrooms { get; set; }
        public int num_baths { get; set; }
        public int num_beds { get; set; }
        public string lonlat { get; set; }
        public string images { get; set; }
        public Apartments apartment { get; set; }
    }
}

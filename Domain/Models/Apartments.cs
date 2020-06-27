using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebHome.Domain.Models
{
    public class Apartments
    {
        public int id { get; set; }
        public string address { get; set; }
        public DateTime free_from { get; set; }
        public DateTime free_to { get; set; }
    }
}

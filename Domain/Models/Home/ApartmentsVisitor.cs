using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebHome.Domain.Models.Home
{
    public class ApartmentsVisitor
    {
        public int num_people { get; set; }
        public string type { get; set; }
        public string address { get; set; }
        public DateTime from { get; set; }
        public DateTime to { get; set; }
    }
}

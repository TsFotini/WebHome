using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebHome.Domain.Models
{
    public class Rate
    {
        public ApartmentsDetails apartment { get; set; }
        public int id { get; set; }
        public int from_user { get; set; }
        public int rate { get; set; }
        public string message_rate { get; set; }
    }
}

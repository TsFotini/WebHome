using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebHome.Domain.IServices;

namespace WebHome.Domain.Models
{
    public class RatesList
    {
        public List<Rate> rates { get; set; }
        public RatesList(IRates Irate)
        {
            rates = Irate.rates();
        }
    }
}

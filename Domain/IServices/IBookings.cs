using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebHome.Domain.Models;

namespace WebHome.Domain.IServices
{
    public interface IBookings
    {
        public List<BookHost> Get_bookings_current_host(int id);
    }
}

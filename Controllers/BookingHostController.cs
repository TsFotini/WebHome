using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using WebHome.Domain.Models;
using WebHome.Domain.Services;

namespace WebHome.Controllers
{
    public class BookingHostController : Controller
    {
        public IActionResult Index()
        {
            return View("/Views/BookingHost.cshtml");
        }

        [HttpGet]
        public List<BookHost> GetBookings(string value)
        {
            var service = new BookingsService();
            var listBookings = service.Get_bookings_current_host(Convert.ToInt32(value));
            return listBookings;
        }

        [HttpPut]
        public IActionResult AcceptBooking([FromBody] string value)
        {
            try
            {
                var db = new DB();
                var Query = @"UPDATE bookings SET closed = 1 where id = @m_id";
                var cmd = new NpgsqlCommand();
                cmd.CommandText = Query;
                cmd.Parameters.AddWithValue("@m_id", Convert.ToInt32(value));
                cmd.Connection = db.npgsqlConnection;
                cmd.ExecuteNonQuery();
                db.close();
            }
            catch(Exception ex)
            {

            }
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteBooking([FromBody]string value)
        {
            try
            {
                var db = new DB();
                var Query = @"DELETE FROM bookings WHERE id = @m_id";
                var cmd = new NpgsqlCommand();
                cmd.CommandText = Query;
                cmd.Parameters.AddWithValue("@m_id", Convert.ToInt32(value));
                cmd.Connection = db.npgsqlConnection;
                cmd.ExecuteNonQuery();
                db.close();
            }
            catch(Exception ex)
            {

            }
            return Ok();
           
        }
    }
}
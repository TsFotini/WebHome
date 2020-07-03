using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Npgsql;
using WebHome.Domain.Models;

namespace WebHome.Controllers
{
    public class BookingController : Controller
    {
        public IActionResult Index()
        {
            return View("/Views/Booking.cshtml");
        }

        [HttpPost]
        public IActionResult Confirm([FromBody] string value)
        {
            try
            {
                Booking values = JsonConvert.DeserializeObject<Booking>(value);
                var db = new DB();
                var Query = @"INSERT INTO bookings (from_user_id, apartment_id, closed, reserved_from, reserved_to) 
                                VALUES (@m_user_id,@m_apartment, @closed, @reserved_from, @reserved_to)";
                var cmd = new NpgsqlCommand();
                cmd.CommandText = Query;
                cmd.Parameters.AddWithValue("@m_user_id", values.from_user_id);
                cmd.Parameters.AddWithValue("@closed", values.closed);
                cmd.Parameters.AddWithValue("@reserved_from", values.reserved_from);
                cmd.Parameters.AddWithValue("@reserved_to", values.reserved_to);
                cmd.Parameters.AddWithValue("@m_apartment", values.apartment_id);
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
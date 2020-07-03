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
    public class MessagesController : Controller
    {
        public IActionResult Index()
        {
            return View("/Views/Messages.cshtml");
        }

        [HttpPost]
        public IActionResult Send([FromBody] string value)
        {
            try
            {
                Message values = JsonConvert.DeserializeObject<Message>(value);
                var db = new DB();
                var Query = @"INSERT INTO messages (from_user_id, to_user_id, message_body, apartment_id) 
                                VALUES (@m_user_id,@to_user_id,@m_message_body,@m_apartment)";
                var cmd = new NpgsqlCommand();
                cmd.CommandText = Query;
                cmd.Parameters.AddWithValue("@m_user_id", values.from_user_id);
                cmd.Parameters.AddWithValue("@to_user_id", values.to_user_id);
                cmd.Parameters.AddWithValue("@m_message_body", values.message_body);
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
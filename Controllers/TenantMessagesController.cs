using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Npgsql;
using WebHome.Domain.Models;
using WebHome.Domain.Services;

namespace WebHome.Controllers
{
    public class TenantMessagesController : Controller
    {
        public static List<Message> new_messages;
        public static Message message;
        public static Message send = new Message();
        public IActionResult Index()
        {
            return View("/Views/TenantMessages.cshtml");
        }

        [HttpGet]
        public List<Message> GetMessages(string value)
        {
            var service = new MessagesService();
            var all_messages = service.Get_Tenant_Messages(Convert.ToInt32(value), "1");
            new_messages = all_messages;
            return all_messages;
        } 

        [HttpGet]
        public List<Message> GetOldMessages(string value)
        {
            var service = new MessagesService();
            var all_messages = service.Get_Tenant_Messages(Convert.ToInt32(value), "Old Messages");
            return all_messages;
        }

        [HttpGet]
        public string GetMessage(string value)
        {
            Message result = new_messages.FirstOrDefault(r => r.id == Convert.ToInt32(value));
            message = result;
            return JsonConvert.SerializeObject(result);
        }

        [HttpPut]
        public IActionResult Seen([FromBody] string value)
        {
            try
            {
                var db = new DB();
                var Query = @"UPDATE messages SET seen = @m_seen where apartment_id = @m_apartment_id and to_user_id = @m_user_id and id = @m_message";
                var cmd = new NpgsqlCommand();
                cmd.CommandText = Query;
                cmd.Parameters.AddWithValue("@m_seen", "https://cdn.onlinewebfonts.com/svg/img_122337.png");
                cmd.Parameters.AddWithValue("@m_apartment_id", message.apartment_id);
                cmd.Parameters.AddWithValue("@m_user_id", message.to_user_id);
                cmd.Parameters.AddWithValue("@m_message", Convert.ToInt32(value));
                cmd.Connection = db.npgsqlConnection;
                cmd.ExecuteNonQuery();
                db.close();
            }
            catch (Exception ex)
            {

            }
            return Ok();
        }
        [HttpPost]
        public IActionResult From([FromBody]string value)
        {
            try
            {
                var service = new UserService();
                int id = service.GetIDofUser(value);
                send.to_user_id = id;
                send.from_user_id = message.to_user_id;
                send.from_username = value;
                send.apartment_id = message.apartment_id;
            }
            catch (Exception ex)
            {

            }
            return Ok();
        }


        [HttpPost]
        public IActionResult Reply([FromBody]string value)
        {
            try
            {
                var db = new DB();
                var Query = @"INSERT INTO messages (from_user_id,to_user_id,message_body,apartment_id) 
                                VALUES (@m_from_user,@m_user_id,@m_message,@m_apartment_id)";
                var cmd = new NpgsqlCommand();
                cmd.CommandText = Query;
                cmd.Parameters.AddWithValue("@m_from_user", send.from_user_id);
                cmd.Parameters.AddWithValue("@m_apartment_id", send.apartment_id);
                cmd.Parameters.AddWithValue("@m_user_id", send.to_user_id);
                send.message_body = value;
                cmd.Parameters.AddWithValue("@m_message", send.message_body);
                cmd.Connection = db.npgsqlConnection;
                cmd.ExecuteNonQuery();
                db.close();
            }
            catch (Exception ex)
            {

            }
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete([FromBody]string value)
        {
            try
            {
                var db = new DB();
                var Query = @"DELETE FROM messages WHERE id = @m_id";
                var cmd = new NpgsqlCommand();
                cmd.CommandText = Query;
                cmd.Parameters.AddWithValue("@m_id", Convert.ToInt32(value));
                cmd.Connection = db.npgsqlConnection;
                cmd.ExecuteNonQuery();
                db.close();
            }
            catch (Exception ex)
            {

            }
            return Ok();

        }
    }
}
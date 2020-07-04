using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Npgsql;
using WebHome.Domain.Models;
using WebHome.Domain.Services;

namespace WebHome.Controllers
{
    public class MessagesApartmentController : Controller
    {
        public static ReceiverMessage receiver = new ReceiverMessage();
        public static Message send = new Message();
        public static List<Message> new_messages;
        public static List<Message> old_messages;
        public IActionResult Index()
        {
            return View("/Views/MessagesApartment.cshtml");
        }

        [HttpPost]
        public ReceiverMessage GetMessagesApartment([FromBody] ReceiverMessage values)
        {
            receiver.apartment_id = values.apartment_id;
            receiver.to_user_id = values.to_user_id;
            return receiver;
        }

        [HttpGet]
        public List<Message> GetMessages()
        {
            var messages = new List<Message>();
            var service = new MessagesService();
            messages = service.Get_Message_Apartment(receiver,"1");
            new_messages = messages;
            return messages;
        }

        [HttpGet]
        public List<Message> GetOldMessages()
        {
            var messages = new List<Message>();
            var service = new MessagesService();
            messages = service.Get_Message_Apartment(receiver,"Old messages");
            old_messages = messages;
            return messages;
        }

        [HttpPut]
        public IActionResult Seen([FromBody] string value )
        {
            try
            {
                var db = new DB();
                var Query = @"UPDATE messages SET seen = @m_seen where apartment_id = @m_apartment_id and to_user_id = @m_user_id and id = @m_message";
                var cmd = new NpgsqlCommand();
                cmd.CommandText = Query;
                cmd.Parameters.AddWithValue("@m_seen", "https://cdn.onlinewebfonts.com/svg/img_122337.png");
                cmd.Parameters.AddWithValue("@m_apartment_id", receiver.apartment_id);
                cmd.Parameters.AddWithValue("@m_user_id", receiver.to_user_id);
                cmd.Parameters.AddWithValue("@m_message", Convert.ToInt32(value));
                cmd.Connection = db.npgsqlConnection;
                cmd.ExecuteNonQuery();
                db.close();
            }
            catch(Exception ex)
            {

            }
            return Ok();
        }

        [HttpGet]
        public string GetMessage(string value)
        {
            Message result = new_messages.FirstOrDefault(r => r.id == Convert.ToInt32(value));
            return JsonConvert.SerializeObject(result);
        }

        [HttpPost]
        public IActionResult From([FromBody]string value)
        {
            try
            {
                var service = new UserService();
                int id = service.GetIDofUser(value);
                send.to_user_id = id;
                send.from_user_id = receiver.to_user_id;
                send.from_username = value;
                send.apartment_id = receiver.apartment_id;
            }
            catch(Exception ex)
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
            catch(Exception ex)
            {

            }
            return Ok();
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using WebHome.Domain.IServices;
using WebHome.Domain.Models;
using WebHome.Domain.Services;

namespace WebHome.Controllers
{
    public class AccountSettingsController : Controller
    {
        public static User current_user;
        public IActionResult Index()
        {
            return View("/Views/AccountSettings.cshtml");
        }

        [HttpGet]
        public User GetCurrUser(string value)
        {
            var usr = new UserService();
            var usr_model = new UserModel(usr, Convert.ToInt32(value));
            current_user = usr_model.user;
            return current_user;
        }

        [HttpPut]
        public JsonResult UpdateUsername([FromBody] string value)
        {
            int res = 1;
            try
            {
                var srvc = new UserService();
                if(srvc.UsernameExists(value) != 1)
                {
                    current_user.usrname = value;
                }
                else
                {
                    res = 0;
                }

            }
            catch(Exception ex)
            {

            }
            var obj = new
            {
                valid = res
            };
            return Json(res);
        }

        [HttpPut]
        public IActionResult UpdatePass([FromBody] string values)
        {
            
            current_user.pass = values;
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateMail([FromBody] string values)
        {
            current_user.email = values;
            return Ok();
        }
        [HttpPut]
        public IActionResult UpdateName([FromBody] string values)
            {
            current_user.name = values;
            return Ok();
        }
        [HttpPut]
        public IActionResult UpdateSurname([FromBody] string values)
        {
            current_user.surname = values;
            return Ok();
        }
        [HttpPut]
        public IActionResult UpdatePhone([FromBody] string values)
        {
            current_user.phone_number = values;
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdatePhoto([FromBody] string values)
        {
            current_user.images = values;
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateRole([FromBody] string values)
        {
            current_user.role_id = Convert.ToInt32(values);
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateAccount()
        {
            try
            {
                var db = new DB();
                var Query = @"Update register set  role_id= @m_role_id , usrname = @m_usrname, pass=@m_pass , name=@m_name, surname=@m_surname,phone_number=@m_phone_number, 
                                images=@m_images,email=@m_email,accepted=@m_accepted where user_id = @m_user_id";
                var cmd = new NpgsqlCommand();
                cmd.CommandText = Query;
                cmd.Parameters.AddWithValue("@m_images", current_user.images);
                cmd.Parameters.AddWithValue("@m_email", current_user.email);
                cmd.Parameters.AddWithValue("@m_phone_number", current_user.phone_number);
                cmd.Parameters.AddWithValue("@m_usrname", current_user.usrname);
                cmd.Parameters.AddWithValue("@m_pass", current_user.pass);
                cmd.Parameters.AddWithValue("@m_name", current_user.name);
                cmd.Parameters.AddWithValue("@m_surname", current_user.surname);
                cmd.Parameters.AddWithValue("@m_user_id", current_user.user_id);
                cmd.Parameters.AddWithValue("@m_role_id", current_user.role_id);
                cmd.Parameters.AddWithValue("@m_accepted", 1);
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
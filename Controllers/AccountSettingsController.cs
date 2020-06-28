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
        public IActionResult UpdateAccount([FromBody] User values)
        {
            try
            {
                var db = new DB();
                var Query = @"Update register set  role_id= @m_role_id , usrname = @m_usrname, pass=@m_pass , name=@m_name, surname=@m_surname,phone_number=@m_phone_number, 
                                images=@m_images,email=@m_email,accepted=@m_accepted where user_id = @m_user_id";
                var cmd = new NpgsqlCommand();
                cmd.CommandText = Query;
                cmd.Parameters.AddWithValue("@m_images", values.images);
                cmd.Parameters.AddWithValue("@m_email", values.email);
                cmd.Parameters.AddWithValue("@m_phone_number", values.phone_number);
                cmd.Parameters.AddWithValue("@m_usrname", values.usrname);
                cmd.Parameters.AddWithValue("@m_pass", values.pass);
                cmd.Parameters.AddWithValue("@m_name", values.name);
                cmd.Parameters.AddWithValue("@m_surname", values.surname);
                cmd.Parameters.AddWithValue("m_created_on", DateTime.Now);
                cmd.Parameters.AddWithValue("@m_user_id", values.user_id);
                cmd.Parameters.AddWithValue("@m_role_id", values.role_id);
                if (values.role_id == 1 || values.role_id == 3)
                {
                    cmd.Parameters.AddWithValue("@m_accepted", 0);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@m_accepted", 1);
                }
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
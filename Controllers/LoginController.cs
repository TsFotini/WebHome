using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using WebHome.Models;

namespace WebHome.Controllers
{
    public class LoginController : Controller
    {
        static Session user_session_created;
        public IActionResult Index()
        {
            return View("/Views/Login.cshtml");
        }
        [HttpPost]
        public IActionResult Authenticate([FromBody]Login values)
        {
            IActionResult response = Ok();
            try
            {
                var db = new DB();
                var data = new List<Login>();
                var Query = @"select usrname,pass,user_id,role_id from register where usrname = @m_usrname and pass = @m_pass and accepted = 1";
                var cmd = new NpgsqlCommand();
                cmd.CommandText = Query;
                cmd.Parameters.AddWithValue("@m_usrname", values.usrname);
                cmd.Parameters.AddWithValue("@m_pass", values.pass);
                cmd.Connection = db.npgsqlConnection;
                cmd.ExecuteNonQuery();
                NpgsqlDataReader DataReader;

                DataReader = cmd.ExecuteReader();

                cmd.Dispose();
                if (DataReader.HasRows)
                {
                    while (DataReader.Read())
                    {
                        data.Add(new Login
                        {
                            usrname = DataReader.GetValue(0).ToString(),
                            pass = DataReader.GetValue(1).ToString(),
                            user_id = Convert.ToInt32(DataReader.GetValue(2)),
                            role_id = Convert.ToInt32(DataReader.GetValue(3))
                        });
                    }


                }
                DataReader.Close();
                DataReader.Dispose();
                db.close();
                if (data.Count > 0)
                {
                    user_session_created = new Session(data[0].user_id, data[0].role_id, data[0].usrname, 1);
                    response = Ok();
                }
                else
                {
                    user_session_created = new Session(-1, -1, "", -1);
                }

            }
            catch (Exception ex)
            {

            }
            return response;
        }

        [HttpGet]
        public JsonResult Log()
        {
            int res = 0;
            if (user_session_created.isloggedIn == 1 && user_session_created.role_id == 0) {
                res = 1;
            }
            var obj = new
            {
                valid = res
            };
            return Json(obj); 
        }
        
        public IActionResult Logout()
        {
            return View("/Views/Register.cshtml");
        }
    }
}
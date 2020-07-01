using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using WebHome.Models;

namespace WebHome.Controllers
{
    public class LoginController : Controller
    {
        public static Session user_session_created = new Session(-1, -1, "", -1);
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
                    History();
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
            else if(user_session_created.isloggedIn == 1 && user_session_created.role_id == 1)
            {
                res = 2; //host 
            }
            var obj = new
            {
                valid = res
            };
            return Json(obj); 
        }

        [HttpPost]
        public void History()
        {
            try
            {
                var db = new DB();
                var Query = @"INSERT INTO login (user_id, last_login) 
                                VALUES (@m_user_id,@m_last_login)";
                var cmd = new NpgsqlCommand();
                cmd.CommandText = Query;
                cmd.Parameters.AddWithValue("@m_user_id", user_session_created.user_id);
                cmd.Parameters.AddWithValue("@m_last_login", DateTime.Now);
                cmd.Connection = db.npgsqlConnection;
                cmd.ExecuteNonQuery();
                db.close();

            }
            catch(Exception ex)
            {

            }
        }

        [HttpGet]
        public string Logout()
        {
            var str = "bye";
            user_session_created = new Session(-1, -1, "", -1);
            return str;
        }
    }
}
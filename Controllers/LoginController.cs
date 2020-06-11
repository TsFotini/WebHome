using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using WebHome.Models;

namespace WebHome.Controllers
{
    public class LoginController : Controller
    {
        [HttpPost]
        public IActionResult Login([FromBody] Login values)
        {
            IActionResult response = Unauthorized();
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
                if(data.Count > 0)
                {
                    response = Ok();
                }

            }
            catch(Exception ex)
            {

            }
            return response;

        }

        public IActionResult Logout()
        {
            return Ok();
        }
    }
}
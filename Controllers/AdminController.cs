using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Npgsql;
using WebHome.Domain.Models;

namespace WebHome.Controllers
{
    public class AdminController : Controller
    { 
        public IActionResult Index()
        {
            return View("/Views/Admin.cshtml");
        }

        [HttpGet] 
        public string GetInfo(string value)
        {
            var data = new List<User>();
            try
            {
                var db = new DB();
                
                var Query = @"select rr.usrname, rr.email, r.description, rr.name, rr.surname, rr.phone_number, rr.images, rr.created_on, rr.accepted
                              from roles r, register rr 
                              where r.role_id = rr.role_id and rr.user_id = @m_user_id";
                var cmd = new NpgsqlCommand();
                cmd.CommandText = Query;
                cmd.Parameters.AddWithValue("@m_user_id", Int32.Parse(value));
                cmd.Connection = db.npgsqlConnection;
                cmd.ExecuteNonQuery();
                NpgsqlDataReader DataReader;

                DataReader = cmd.ExecuteReader();

                cmd.Dispose();
                if (DataReader.HasRows)
                {
                    while (DataReader.Read())
                    {
                        data.Add(new User
                        {
                            usrname = DataReader.GetValue(0).ToString(),
                            email = DataReader.GetValue(1).ToString(),
                            role = DataReader.GetValue(2).ToString(),
                            name = DataReader.GetValue(3).ToString(),
                            surname = DataReader.GetValue(4).ToString(),
                            phone_number = DataReader.GetValue(5).ToString(),
                            images = DataReader.GetValue(6).ToString(),
                            created_on = Convert.ToDateTime(DataReader.GetValue(7)),
                            accepted = Convert.ToInt32(DataReader.GetValue(8)),
                        }) ;
                    }


                }
                DataReader.Close();
                DataReader.Dispose();
                db.close();

            }
            catch(Exception ex)
            {

            }
            return JsonConvert.SerializeObject(data);
        }

        [HttpPut]
        public IActionResult SetAccepted([FromBody]int value)
        {
            try
            {
                var db = new DB();
                var Query = @"UPDATE register
                                SET accepted = 1
                                WHERE
	                            user_id = @m_user_id";
                var cmd = new NpgsqlCommand();
                cmd.CommandText = Query;
                cmd.Parameters.AddWithValue("@m_user_id", value);
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
        public string GetUsers()
        {
            var obj1 = new List<List<string>>();
            try
            {
                var db = new DB();
                var Query = @"select user_id, usrname, email, accepted,images from register";
                var cmd = new NpgsqlCommand();
                cmd.CommandText = Query;
                cmd.Connection = db.npgsqlConnection;
                NpgsqlDataReader DataReader;

                DataReader = cmd.ExecuteReader();

                cmd.Dispose();
                if (DataReader.HasRows)
                {
                    
                    while (DataReader.Read())
                    {
                        var obj = new List<string>();
                        obj.Add(DataReader.GetValue(0).ToString());
                        obj.Add(DataReader.GetValue(1).ToString());
                        obj.Add(DataReader.GetValue(2).ToString());
                        obj.Add(DataReader.GetValue(3).ToString());
                        var img = "<img src=' " + DataReader.GetValue(4).ToString() + " ' style='height:80px; width:80px'>";
                        obj.Add(img);
                        obj1.Add(obj);
                    }
                   

                }
                DataReader.Close();
                DataReader.Dispose();
                db.close();
            }
            catch(Exception ex)
            {

            }
            var str = JsonConvert.SerializeObject(obj1);
            return str;
        }

        [HttpDelete]
        public IActionResult Delete([FromBody]string value)
        {
            try
            {
                var db = new DB();
                var Query = @"DELETE FROM register WHERE user_id = @m_id";
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
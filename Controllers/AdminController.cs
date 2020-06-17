using System;
using System.Collections.Generic;
using System.Linq;
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
                
                var Query = @"select rr.usrname, rr.email, r.description, rr.name, rr.surname, rr.phone_number, rr.images, rr.created_on 
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
                            created_on = Convert.ToDateTime(DataReader.GetValue(7))
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

        [HttpGet]
        public string GetUsers()
        {
            var obj1 = new List<List<string>>();
            try
            {
                var db = new DB();
                var Query = @"select user_id, usrname, email, accepted from register";
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
    }
}
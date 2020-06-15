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
    public class AdminController : Controller
    { 
        public IActionResult Index()
        {
            return View("/Views/Admin.cshtml");
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
            return JsonConvert.SerializeObject(obj1);
        }
    }
}
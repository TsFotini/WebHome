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
    public class ApartmentsController : Controller
    {
        public IActionResult Index()
        {
            return View("/Views/Apartments.cshtml");
        }

        [HttpGet] 
        public string GetApartments(string value)
        {
            var data = new List<Apartments>();
            try
            {
                var db = new DB();
                var Query = @"select ap.id, ap.address, ap.free_from, ap.free_to , apt.description from apartments ap, apartment_type apt where user_id = @:m_user_id and ap.type_id = apt.id";
                var cmd = new NpgsqlCommand();
                cmd.CommandText = Query;
                cmd.Parameters.AddWithValue("@m_user_id", Int32.Parse(value));
                cmd.Connection = db.npgsqlConnection;
                NpgsqlDataReader DataReader;

                DataReader = cmd.ExecuteReader();

                cmd.Dispose();
                if (DataReader.HasRows)
                {

                    while (DataReader.Read())
                    {
                        data.Add(new Apartments
                        {
                            id = Convert.ToInt32(DataReader.GetValue(0)),
                            address = DataReader.GetValue(1).ToString(),
                            free_from = Convert.ToDateTime(DataReader.GetValue(2)),
                            free_to = Convert.ToDateTime(DataReader.GetValue(3)),
                            type_description = DataReader.GetValue(4).ToString()
                        });
                    }


                }
                DataReader.Close();
                DataReader.Dispose();
                db.close();

            }
            catch(Exception ex)
            {

            }
            var str = JsonConvert.SerializeObject(data);
            return str;
        }
    }
}
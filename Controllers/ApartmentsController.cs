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
                var Query = @"select id, address, free_from, free_to  from apartments where user_id = @m_user_id";
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

        [HttpPost]
        public IActionResult InsertApartment([FromBody] ApartmentsDetails values)
        {
            try
            {
                var db = new DB();
                var Query = @"INSERT INTO apartments (user_id, address, reach_place, free_from, free_to, max_people,min_price, cost_per_person,description,num_beds,num_baths,num_bedrooms,area,rules,images,lonlat, type_description) 
                                VALUES (@m_user_id, @m_address, @m_reach_place, @m_free_from, @m_free_to, @m_max_people,@m_min_price, @m_cost_per_person,@m_description,@m_num_beds,@m_num_baths,@m_num_bedrooms,@m_area,@m_rules,@m_images,@m_lonlat,@m_type_description)";
                var cmd = new NpgsqlCommand();
                cmd.CommandText = Query;
                cmd.Parameters.AddWithValue("@m_user_id", values.user_id);
                cmd.Parameters.AddWithValue("@m_address", values.apartment.address);
                cmd.Parameters.AddWithValue("@m_reach_place", values.reach_place);
                cmd.Parameters.AddWithValue("@m_free_from", values.apartment.free_from);
                cmd.Parameters.AddWithValue("@m_free_to", values.apartment.free_to);
                cmd.Parameters.AddWithValue("@m_max_people", values.max_people);
                cmd.Parameters.AddWithValue("@m_min_price", values.min_price);
                cmd.Parameters.AddWithValue("m_cost_per_person", values.cost_per_person);
                cmd.Parameters.AddWithValue("@m_description", values.description);
                cmd.Parameters.AddWithValue("@m_num_beds", values.num_beds);
                cmd.Parameters.AddWithValue("@m_num_baths", values.num_baths);
                cmd.Parameters.AddWithValue("@m_num_bedrooms", values.num_bedrooms);
                cmd.Parameters.AddWithValue("@m_area", values.area);
                cmd.Parameters.AddWithValue("@m_rules", values.rules);
                cmd.Parameters.AddWithValue("@m_images", values.images);
                cmd.Parameters.AddWithValue("@m_lonlat", values.lonlat);
                cmd.Parameters.AddWithValue("@m_type_description", values.type_description);
                cmd.Connection = db.npgsqlConnection;
                cmd.ExecuteNonQuery();
                db.close();
            }
            catch(Exception Ex)
            {

            }
            return View("/Views/Apartments.cshtml");
        }
    }
}
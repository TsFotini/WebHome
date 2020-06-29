using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Npgsql;
using WebHome.Domain.IServices;
using WebHome.Domain.Models;
using WebHome.Domain.Services;

namespace WebHome.Controllers
{
    public class ApartmentsDetailsController : Controller
    {
        public static ApartmentsDetails apartment = new ApartmentsDetails();
        public IActionResult Index()
        {
            return View("/Views/ApartmentsDetails.cshtml");
        }

        [HttpGet]
        public ApartmentsDetails GetInfoApartments(string value)
        {
            var ap = new ApartmentsListService();
            var apartments = new ApartmentsList(ap);
            var current_apartment = new ApartmentsDetails();
            try
            {
                current_apartment = apartments.GetByID(Convert.ToInt32(value));
            }
            catch(Exception ex)
            {

            }
            apartment = current_apartment;
            return current_apartment;

        }

        [HttpPut]
        public IActionResult UpdatePlace([FromBody] string value)
        {
            try
            {
                ApartmentsDetails values = JsonConvert.DeserializeObject<ApartmentsDetails>(value);
                var db = new DB();
                var Query = @"UPDATE apartments SET address = @m_address, reach_place = @m_reach_place, free_from = @m_free_from, free_to = @m_free_to, " +
                    "max_people = @m_max_people, min_price= @m_min_price, cost_per_person= @m_cost_per_person, description= @m_description, " +
                    "num_beds= @m_num_beds, num_baths= @m_num_baths, num_bedrooms= @m_num_bedrooms, area= @m_area, " +
                    "rules= @m_rules, images= @m_images, lonlat= @m_lonlat, type_description= @m_type_description WHERE id = @m_id"; 
                var cmd = new NpgsqlCommand();
                cmd.CommandText = Query;
                cmd.Parameters.AddWithValue("@m_id", values.apartment.id);
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
            catch(Exception ex)
            {

            }
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeletePlace([FromBody] string value)
        {
            var ap = new ApartmentsListService();
            var apartments = new ApartmentsList(ap);
            try
            {
                apartments.DeleteApartment(Convert.ToInt32(value),ap);
                
            }
            catch(Exception ex)
            {

            }
            return Ok();
        }
    }
}
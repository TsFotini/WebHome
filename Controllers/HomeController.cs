﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Npgsql;
using WebHome.Domain.Models;
using WebHome.Domain.Models.Home;
using WebHome.Models;

namespace WebHome.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login([FromBody] Login values)
        {
            
            return Ok();
            
        }

        [HttpGet]
        public string GetApartmentsTypes()
        {
            string jsonObject = "";
            try
            {
                var data = new List<ApartmentType>();
                
                var cs = "Host=localhost;Username=postgres;Password=postgres;Database=mydata";

                var npgsqlConnection = new NpgsqlConnection(cs);
                npgsqlConnection.Open();
                var Query = @"select id,description from apartment_type";
                var cmd = new NpgsqlCommand();
                cmd.CommandText = Query;
                cmd.Connection = npgsqlConnection;
                NpgsqlDataReader DataReader;
               
                DataReader = cmd.ExecuteReader();

                cmd.Dispose();
                if (DataReader.HasRows)
                {
                    while (DataReader.Read())
                    {
                        data.Add(new ApartmentType
                        {
                            id = Convert.ToInt32(DataReader.GetValue(0)),
                            description = DataReader.GetValue(1).ToString(),
                        });
                    }


                }
                DataReader.Close();
                DataReader.Dispose();
                data = data.OrderByDescending(a => a.id).ToList();
                jsonObject = JsonConvert.SerializeObject(data);

            }
            catch (Exception ex)
            {

            }
            return jsonObject;
        }



    }
}

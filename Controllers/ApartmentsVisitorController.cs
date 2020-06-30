using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Npgsql;
using WebHome.Domain.Models;
using WebHome.Domain.Models.Home;
using WebHome.Domain.Services;

namespace WebHome.Controllers
{
    public class ApartmentsVisitorController : Controller
    {
        public static ApartmentsVisitor apartments_info_model;
        public IActionResult Index()
        {
            return View("/Views/ApartmentsVisitor.cshtml");
        }

        [HttpPost]
        public ApartmentsVisitor CatchInfos([FromBody] string values)
        {
            apartments_info_model = JsonConvert.DeserializeObject<ApartmentsVisitor>(values);
            return apartments_info_model;
        }

        [HttpGet]
        public string GetApartments()
        {
            var data = new List<Rate>();
            try
            {
                var service_item = new RatesService();
                var rates_list = new RatesList(service_item);
                data = rates_list.rates;
            }
            catch(Exception ex)
            {

            }
            var str = JsonConvert.SerializeObject(data);
            return str;
        }
    }
}
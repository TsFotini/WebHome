using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
    }
}
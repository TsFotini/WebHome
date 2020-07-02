using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebHome.Domain.Models;
using WebHome.Domain.Services;

namespace WebHome.Controllers
{
    public class ApartmentRentController : Controller
    {
        public static ApartmentsDetails apartment;

        public static User host = new User();

        public IActionResult Index()
        {
            return View("/Views/ApartmentRent.cshtml");
        }

        [HttpGet]
        public ApartmentsDetails GetApartment(string value)
        {
            var service = new ApartmentsListService();
            var all = new ApartmentsList(service);
            apartment = all.GetByID(Convert.ToInt32(value));
            return apartment;
        }

        [HttpGet]
        public User GetUserHost()
        {
            var service = new UserService();
            var user_host = new UserModel(service, apartment.user_id);
            host = user_host.user;
            return host;
        }

    }
}
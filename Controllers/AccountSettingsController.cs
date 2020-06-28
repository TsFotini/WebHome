using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebHome.Domain.IServices;
using WebHome.Domain.Models;
using WebHome.Domain.Services;

namespace WebHome.Controllers
{
    public class AccountSettingsController : Controller
    {
        public static User current_user;
        public IActionResult Index()
        {
            return View("/Views/AccountSettings.cshtml");
        }

        [HttpGet]
        public User GetCurrUser(string value)
        {
            var usr = new UserService();
            var usr_model = new UserModel(usr, Convert.ToInt32(value));
            current_user = usr_model.user;
            return current_user;
        }
    }
}
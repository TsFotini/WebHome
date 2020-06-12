using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebHome.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View("/Views/Admin.cshtml");
        }
    }
}
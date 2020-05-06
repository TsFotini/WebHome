using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using WebHome.Domain.Models;

namespace WebHome.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        [HttpGet]
        public IActionResult CreateAccount([FromBody] Register values)
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=postgres;Database=mydata";

                var npgsqlConnection = new NpgsqlConnection(cs);
                npgsqlConnection.Open();

                var  Query = @"Insert into ";

               

                var version = cmd.ExecuteScalar().ToString();
            }
            catch(Exception ex)
            {

            }
            return Ok();
        }
    }
}
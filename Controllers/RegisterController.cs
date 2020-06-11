using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using WebHome.Domain.Models;

namespace WebHome.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View("/Views/Register.cshtml");
        }

        [HttpPost]
        public IActionResult Register([FromBody] Register values)
        {
            try
            {
                var db = new DB();
                int id = Create_Id(db.npgsqlConnection);
                var Query = @"INSERT INTO register (user_id, role_id, usrname, pass, name, surname,phone_number, images,created_on,email,accepted) 
                                VALUES (@m_user_id,@m_role_id,@m_usrname,@m_pass,@m_name,@m_surname,@m_phone_number,@m_images,@m_created_on,@m_email,@m_accepted)";
                var cmd = new  NpgsqlCommand();
                cmd.CommandText = Query;
                cmd.Parameters.AddWithValue("@m_images", values.images);
                cmd.Parameters.AddWithValue("@m_email", values.email);
                cmd.Parameters.AddWithValue("@m_phone_number", values.phone_number);
                cmd.Parameters.AddWithValue("@m_usrname", values.usrname);
                cmd.Parameters.AddWithValue("@m_pass", values.pass);
                cmd.Parameters.AddWithValue("@m_name", values.name);
                cmd.Parameters.AddWithValue("@m_surname", values.surname);
                cmd.Parameters.AddWithValue("m_created_on", DateTime.Now);
                cmd.Parameters.AddWithValue("@m_user_id", id);
                cmd.Parameters.AddWithValue("@m_role_id", values.role_id);
                if(values.role_id == 1 || values.role_id == 3)
                {
                    cmd.Parameters.AddWithValue("@m_accepted", 0);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@m_accepted", 1);
                }
                cmd.Connection = db.npgsqlConnection;
                cmd.ExecuteNonQuery();
                db.close();
            }
            catch (Exception ex)
            {

            }
            return Ok();
        }

        public int Create_Id(NpgsqlConnection npgsqlConnection)
        {
            int id = -1;
            try
            {
                
                var Cmd = new NpgsqlCommand();
                var Query = @"select max(user_id) from register";
                Cmd.CommandText = Query;
                Cmd.Connection = npgsqlConnection;
                NpgsqlDataReader DataReader;
            
                DataReader = Cmd.ExecuteReader();

                Cmd.Dispose();
                if (DataReader.HasRows)
                {
                        while (DataReader.Read())
                        {
                            var paramValue = DataReader.GetValue(0);
                            if (!(paramValue is DBNull))
                                id = Convert.ToInt32(DataReader.GetValue(0));
                            else
                                id = -1;
                        }

                }
                DataReader.Close();
                DataReader.Dispose();

            }
            catch(Exception ex)
            {

            }

            return id+1;
        }

        [HttpGet]
        public JsonResult UsrExist(string value)
        {
            bool result = false;
            int res = 0;
           
            try
            {
                var db = new DB();
                var Query = @"select exists(select 1 from register where usrname = @m_usrname)";
                var cmd = new NpgsqlCommand();
                cmd.CommandText = Query;
                cmd.Parameters.AddWithValue("@m_usrname", value);
                cmd.Connection = db.npgsqlConnection;
                cmd.ExecuteNonQuery();
                
                NpgsqlDataReader DataReader;

                DataReader = cmd.ExecuteReader();

                cmd.Dispose();
                if (DataReader.HasRows)
                {
                    while (DataReader.Read())
                    {
                        result = Convert.ToBoolean(DataReader.GetValue(0));
                    }

                }
                DataReader.Close();
                DataReader.Dispose();
                if(result == true)
                {
                    res = 1;
                }
                else
                {
                    res = 0;
                }
                db.close();
            }
            catch(Exception ex)
            {

            }
            var obj = new
            {
                valid = res
            };
            return Json(obj);
        }

    }
}
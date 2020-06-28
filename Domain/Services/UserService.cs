using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebHome.Domain.IServices;
using WebHome.Domain.Models;

namespace WebHome.Domain.Services
{
    public class UserService : IUser
    {
        public  User user_details;

        public User GetUser(int id)
        {
            var user = new User();
            try
            {
                var db = new DB();
                var Query = @"select r.user_id, r.role_id, r.usrname, r.pass, r.name, r.surname,r.phone_number, r.images,r.email, rr.description from roles rr, register r
                              where r.role_id = rr.role_id and r.user_id = @m_user_id";
                var cmd = new NpgsqlCommand();
                cmd.CommandText = Query;
                cmd.Parameters.AddWithValue("@m_user_id", id);
                cmd.Connection = db.npgsqlConnection;
                NpgsqlDataReader DataReader;
                DataReader = cmd.ExecuteReader();

                cmd.Dispose();
                
                if (DataReader.HasRows)
                {
                    while (DataReader.Read())
                    {
                        user.user_id = Convert.ToInt32(DataReader.GetValue(0));
                        user.role_id = Convert.ToInt32(DataReader.GetValue(1));
                        user.usrname = DataReader.GetValue(2).ToString();
                        user.pass = DataReader.GetValue(3).ToString();
                        user.name = DataReader.GetValue(4).ToString();
                        user.surname = DataReader.GetValue(5).ToString();
                        user.phone_number = DataReader.GetValue(6).ToString();
                        user.images = DataReader.GetValue(7).ToString();
                        user.email = DataReader.GetValue(8).ToString();
                        user.role = DataReader.GetValue(9).ToString();
                    }

                }
                DataReader.Close();
                DataReader.Dispose();
                db.close();
            }
            catch(Exception ex)
            {

            }
            return user;
        }


    }
}

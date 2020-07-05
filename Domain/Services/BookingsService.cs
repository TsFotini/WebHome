using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebHome.Domain.IServices;
using WebHome.Domain.Models;

namespace WebHome.Domain.Services
{
    public class BookingsService : IBookings
    {
        public List<BookHost> Get_bookings_current_host(int id)
        {
            var data = new List<BookHost>();
            try
            {
                var db = new DB();
                var Query = "";
                Query = @"select b.id,r.usrname,r.images,b.from_user_id,b.apartment_id,a.images,b.reserved_from,b.reserved_to,b.closed from bookings b, register r, apartments a 
                            where b.from_user_id = r.user_id 
                            and b.apartment_id = a.id and a.user_id = @m_user_id";
              
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
                        data.Add(new BookHost
                        {
                            id = Convert.ToInt32(DataReader.GetValue(0)),
                            from_username = DataReader.GetValue(1).ToString(),
                            image_user = DataReader.GetValue(2).ToString(),
                            from_user_id = Convert.ToInt32(DataReader.GetValue(3)),
                            apartment_id = Convert.ToInt32(DataReader.GetValue(4)),
                            image_apartment = DataReader.GetValue(5).ToString(),
                            reserved_from = Convert.ToDateTime(DataReader.GetValue(6)),
                            reserved_to = Convert.ToDateTime(DataReader.GetValue(7)),
                            closed = Convert.ToInt32(DataReader.GetValue(8))
                        }) ;
                    }


                }
                DataReader.Close();
                DataReader.Dispose();
                db.close();
                for (int i = 0; i < data.Count(); i++)
                {
                    data[i].image_user = "<img src=' " + data[i].image_user + " ' style='height:80px; width:80px'>";
                    data[i].image_apartment = "<img src=' " + data[i].image_apartment + " ' style='height:80px; width:80px'>";
                }
            }
            catch(Exception ex)
            {

            }
            return data;
        }
    }
}

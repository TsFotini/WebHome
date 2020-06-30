using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebHome.Domain.IServices;
using WebHome.Domain.Models;

namespace WebHome.Domain.Services
{
    public class RatesService : IRates
    {
        public List<Rate> rates()
        {
            var data = new List<Rate>();
            try
            {
                var db = new DB();
                var Query = @"select ap.id, ap.images, ap.min_price, ap.type_description, ap.num_beds, r.message_rate,r.rate from rate r  
                                                                                                            Right JOIN apartments ap ON (r.apartment_id = ap.id)";
                var cmd = new NpgsqlCommand();
                cmd.CommandText = Query;
                cmd.Connection = db.npgsqlConnection;
                NpgsqlDataReader DataReader;

                DataReader = cmd.ExecuteReader();

                cmd.Dispose();
                if (DataReader.HasRows)
                {

                    while (DataReader.Read())
                    {
                        var ap = new Apartments();
                        ap.id = Convert.ToInt32(DataReader.GetValue(0));
                        var apartment = new ApartmentsDetails();
                        var imagestring = "<img src=' " + DataReader.GetValue(1).ToString() + " ' style='height:30px; width:30px'>";
                        apartment.images = imagestring;
                        apartment.min_price = Convert.ToSingle(DataReader.GetValue(2));
                        apartment.type_description = DataReader.GetValue(3).ToString();
                        apartment.num_beds = Convert.ToInt32(DataReader.GetValue(4));
                        apartment.apartment = ap;
                        var temp = 0;
                        if (DataReader["rate"] != DBNull.Value)
                            temp = Convert.ToInt32(DataReader.GetValue(6));
                        data.Add(new Rate
                        {
                            message_rate = DataReader.GetValue(5).ToString(),
                            apartment = apartment,
                            rate = temp
                        }) ;
                    }


                }
                DataReader.Close();
                DataReader.Dispose();
                db.close();


            }
            catch (Exception ex)
            {

            }
            return data;
        }
    }
}

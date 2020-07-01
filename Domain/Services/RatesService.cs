using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebHome.Domain.IServices;
using WebHome.Domain.Models;
using WebHome.Domain.Models.Home;

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
                var Query = @"select ap.id, ap.images, ap.min_price, ap.type_description, ap.num_beds, r.message_rate,r.rate, ap.free_from, ap.free_to,ap.address, ap.num_baths,ap.num_bedrooms, ap.area,ap.cost_per_person from rate r  
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
                        ap.free_from = Convert.ToDateTime(DataReader.GetValue(7));
                        ap.free_to = Convert.ToDateTime(DataReader.GetValue(8));
                        ap.address = DataReader.GetValue(9).ToString();
                        var apartment = new ApartmentsDetails();
                        var imagestring = "<img src=' " + DataReader.GetValue(1).ToString() + " ' style='height:80px; width:80px'>";
                        apartment.images = imagestring;
                        apartment.min_price = Convert.ToSingle(DataReader.GetValue(2));
                        apartment.type_description = DataReader.GetValue(3).ToString();
                        apartment.num_beds = Convert.ToInt32(DataReader.GetValue(4));
                        apartment.num_baths = Convert.ToInt32(DataReader.GetValue(10));
                        apartment.num_bedrooms = Convert.ToInt32(DataReader.GetValue(11));
                        apartment.area = Convert.ToInt32(DataReader.GetValue(12));
                        apartment.cost_per_person = Convert.ToSingle(DataReader.GetValue(13));
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
                data = data.OrderByDescending(a => a.apartment.min_price).ToList();

            }
            catch (Exception ex)
            {

            }
            return data;
        }

        public List<Rate> filter_list_visitor(List<Rate> rates, ApartmentsVisitor a)
        {
            var filteredlist = rates;
            if(a != null) {
                filteredlist = rates.Where(p => p.apartment.type_description == a.type || p.apartment.apartment.address == a.address || p.apartment.max_people == a.num_people ||
                                                    (p.apartment.apartment.free_from >= a.from && p.apartment.apartment.free_to <= a.to)).ToList();
                filteredlist = filteredlist.OrderByDescending(a => a.apartment.min_price).ToList();
            }
            
            return filteredlist;
        }
    }
}

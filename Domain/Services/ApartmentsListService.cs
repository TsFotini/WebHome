using Microsoft.AspNetCore.Mvc.Diagnostics;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebHome.Domain.IServices;
using WebHome.Domain.Models;

namespace WebHome.Domain.Services
{
    public class ApartmentsListService : IApartmentsList
    {
        public List<ApartmentsDetails> data = new List<ApartmentsDetails>();
        public List<ApartmentsDetails>GetApartments()
        {
           
            try
            {

                var db = new DB();
                var Query = @"select user_id, id, address, reach_place, free_from, free_to, max_people,min_price, cost_per_person,description,num_beds,num_baths,
                                    num_bedrooms,area,rules,images,lonlat, type_description from apartments";
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
                        var apartmentObject = new Apartments();
                        apartmentObject.id = Convert.ToInt32(DataReader.GetValue(1));
                        apartmentObject.address = DataReader.GetValue(2).ToString();
                        apartmentObject.free_from = Convert.ToDateTime(DataReader.GetValue(4));
                        apartmentObject.free_to = Convert.ToDateTime(DataReader.GetValue(5));
                        data.Add(new ApartmentsDetails
                        {
                            user_id = Convert.ToInt32(DataReader.GetValue(0)),
                            reach_place = DataReader.GetValue(3).ToString(),
                            max_people = Convert.ToInt32(DataReader.GetValue(6)),
                            min_price = Convert.ToSingle(DataReader.GetValue(7)),
                            cost_per_person = Convert.ToSingle(DataReader.GetValue(8)),
                            description = DataReader.GetValue(9).ToString(),
                            num_beds = Convert.ToInt32(DataReader.GetValue(10)),
                            num_baths = Convert.ToInt32(DataReader.GetValue(11)),
                            num_bedrooms = Convert.ToInt32(DataReader.GetValue(12)),
                            area = Convert.ToInt32(DataReader.GetValue(13)),
                            rules = DataReader.GetValue(14).ToString(),
                            images = DataReader.GetValue(15).ToString(),
                            lonlat = DataReader.GetValue(16).ToString(),
                            type_description = DataReader.GetValue(17).ToString(),
                            apartment = apartmentObject

                        }) ;
                    }


                }
                DataReader.Close();
                DataReader.Dispose();
                data = data.OrderByDescending(a => a.user_id).ToList();
                db.close();
            }
            catch (Exception ex)
            {

            }
            return data;
        }
    }
}

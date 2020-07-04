using Microsoft.VisualBasic.CompilerServices;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebHome.Domain.IServices;
using WebHome.Domain.Models;

namespace WebHome.Domain.Services
{
    public class MessagesService : IMessages
    {
        public List<Message> Get_Message_Apartment(ReceiverMessage receiver, string type)
        {
            var data = new List<Message>();
            try
            {
                var db = new DB();
                var Query = "";
                if(type == "1")
                {
                    Query = @"select m.id,r.usrname,m.message_body,m.from_user_id, r.images, m.seen from messages m, register r where m.from_user_id = r.user_id and apartment_id = @m_apartment_id and to_user_id = @m_user_id and seen is null";
                }
                else
                {
                    Query = @"select m.id,r.usrname,m.message_body,m.from_user_id, r.images, m.seen from messages m, register r where m.from_user_id = r.user_id and apartment_id = @m_apartment_id and to_user_id = @m_user_id and seen is not null";
                }
               
                var cmd = new NpgsqlCommand();
                cmd.CommandText = Query;
                cmd.Parameters.AddWithValue("@m_user_id", receiver.to_user_id);
                cmd.Parameters.AddWithValue("@m_apartment_id", receiver.apartment_id);
                cmd.Connection = db.npgsqlConnection;
                NpgsqlDataReader DataReader;
                DataReader = cmd.ExecuteReader();

                cmd.Dispose();

                if (DataReader.HasRows)
                {
                    while (DataReader.Read())
                    {
                        data.Add(new Message
                        {
                            id = Convert.ToInt32(DataReader.GetValue(0)),
                            from_username = DataReader.GetValue(1).ToString(),
                            message_body = DataReader.GetValue(2).ToString(),
                            from_user_id = Convert.ToInt32(DataReader.GetValue(3)),
                            images = DataReader.GetValue(4).ToString(),
                            seen = DataReader.GetValue(5).ToString(),
                        });
                    }
                        

                }
                DataReader.Close();
                DataReader.Dispose();
                db.close();
                if (type == "1")
                {
                    for (int i = 0; i < data.Count(); i++)
                    {
                        data[i].images = "<img src=' " + data[i].images + " ' style='height:80px; width:80px'>";
                        data[i].seen = "<img src=' " + "https://cdn4.iconfinder.com/data/icons/starup-business-2/120/hide_unseen_seen_eye_blind-512.png" + " ' style='height:20px; width:20px'>";
                    }
                }
                else
                {
                    for (int i = 0; i < data.Count(); i++)
                    {
                        data[i].images = "<img src=' " + data[i].images + " ' style='height:80px; width:80px'>";
                        data[i].seen = "<img src=' " + data[i].seen + " ' style='height:30px; width:30px'>";
                    }
                }
                

            }
            catch (Exception ex)
            {

            }
            return data;
        }

        public List<Message> Get_Tenant_Messages(int id,string type)
        {
            var data = new List<Message>();
            try
            {
                var db = new DB();
                var Query = "";
                if (type == "1")
                {
                    Query = @"select m.id,r.usrname,m.message_body,m.from_user_id, r.images, m.seen, m.apartment_id, a.images,m.to_user_id from messages m, register r, apartments a 
                                where m.from_user_id = r.user_id and m.to_user_id = @user_id and m.apartment_id = a.id and seen is null";
                }
                else
                {
                    Query = @"select m.id,r.usrname,m.message_body,m.from_user_id, r.images, m.seen, m.apartment_id, a.images,m.to_user_id from messages m, register r, apartments a 
                                where m.from_user_id = r.user_id and m.to_user_id = @user_id and m.apartment_id = a.id and seen is not null";
                }

                var cmd = new NpgsqlCommand();
                cmd.CommandText = Query;
                cmd.Parameters.AddWithValue("@user_id", id);
                cmd.Connection = db.npgsqlConnection;
                NpgsqlDataReader DataReader;
                DataReader = cmd.ExecuteReader();

                cmd.Dispose();

                if (DataReader.HasRows)
                {
                    while (DataReader.Read())
                    {
                        data.Add(new Message
                        {
                            id = Convert.ToInt32(DataReader.GetValue(0)),
                            from_username = DataReader.GetValue(1).ToString(),
                            message_body = DataReader.GetValue(2).ToString(),
                            from_user_id = Convert.ToInt32(DataReader.GetValue(3)),
                            images = DataReader.GetValue(4).ToString(),
                            seen = DataReader.GetValue(5).ToString(),
                            apartment_id = Convert.ToInt32(DataReader.GetValue(6)),
                            image_apartment = DataReader.GetValue(7).ToString(),
                            to_user_id = Convert.ToInt32(DataReader.GetValue(8))
                        }) ;
                    }


                }
                DataReader.Close();
                DataReader.Dispose();
                db.close();
                if (type == "1")
                {
                    for (int i = 0; i < data.Count(); i++)
                    {
                        data[i].images = "<img src=' " + data[i].images + " ' style='height:80px; width:80px'>";
                        data[i].seen = "<img src=' " + "https://cdn4.iconfinder.com/data/icons/starup-business-2/120/hide_unseen_seen_eye_blind-512.png" + " ' style='height:20px; width:20px'>";
                        data[i].image_apartment = "<img src=' " + data[i].image_apartment + " ' style='height:80px; width:80px'>";
                    }
                }
                else
                {
                    for (int i = 0; i < data.Count(); i++)
                    {
                        data[i].images = "<img src=' " + data[i].images + " ' style='height:80px; width:80px'>";
                        data[i].seen = "<img src=' " + data[i].seen + " ' style='height:30px; width:30px'>";
                        data[i].image_apartment = "<img src=' " + data[i].image_apartment + " ' style='height:80px; width:80px'>";
                    }
                }


            }
            catch (Exception ex)
            {

            }
            return data;
        }

        
    }
}

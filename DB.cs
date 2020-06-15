using Microsoft.AspNetCore.SignalR.Protocol;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebHome.Properties;

namespace WebHome
{
    public class DB
    {
        public NpgsqlConnection npgsqlConnection { get; }

        public static string cs;
        public DB()
        {
            cs = Resources.cs;
            var npgsqlConnection1 = new NpgsqlConnection(cs);
            npgsqlConnection1.Open();
            npgsqlConnection = npgsqlConnection1;
        }
        public void close()
        {
            npgsqlConnection.Close();
        }
    }
}

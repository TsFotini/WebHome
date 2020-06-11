using Microsoft.AspNetCore.SignalR.Protocol;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebHome
{
    public class DB
    {
        public NpgsqlConnection npgsqlConnection { get; }
        public DB()
        {
            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=mydata";

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

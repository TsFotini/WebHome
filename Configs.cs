using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebHome.Properties;

namespace WebHome
{
    public class Configs
    {
        public static string WebSiteUrl;
        public Configs()
        {
            var json = File.ReadAllText("WebHome/Properties/launchSettings.json");
            var jObject = JObject.Parse(json);
            var port = jObject["sslPort"].ToString();
            WebSiteUrl = Resources.websiteurl + port;
        }

    }
}

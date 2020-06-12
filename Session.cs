using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebHome
{
    public class Session
    {
        public  int user_id { get; set; }
        public  int role_id { get; set; }
        public  string usrname { get; set; }
        public  int isloggedIn { get; set; }

        public Session(int userid,int roleid,string username,int islogged)
        {
            user_id = userid;
            role_id = roleid;
            usrname = username;
            isloggedIn = islogged;
        }
    }
}

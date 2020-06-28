using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebHome.Domain.Models;

namespace WebHome.Domain.IServices
{
    public interface IUser
    {
        public User GetUser(int id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using WebHome.Domain.Models;
using WebHome.Domain.Models.Home;

namespace WebHome.Domain.IServices
{
    public interface IRates
    {
        public List<Rate> rates();
        public List<Rate> filter_list_visitor(List<Rate> rates, ApartmentsVisitor a);
    }
}

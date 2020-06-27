using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebHome.Domain.IServices;

namespace WebHome.Domain.Models
{
    public class ApartmentsList
    {
        public List<ApartmentsDetails> data { get; set; }

        public ApartmentsList(IApartmentsList IApartment)
        {
            data = IApartment.GetApartments();
        }
        public ApartmentsDetails GetByID(int this_id)
        {
            ApartmentsDetails result = data.FirstOrDefault(r => r.apartment.id == this_id);
            return result;
        }
    }
}
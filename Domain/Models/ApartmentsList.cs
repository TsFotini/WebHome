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

        public List<ApartmentsDetails> DeleteApartment(int ID,IApartmentsList IApartment)
        {
            int id = IApartment.DeleteApartment(ID);
            var item = data.SingleOrDefault(x => x.apartment.id == id);
            if (item != null)
                data.Remove(item);
            return data;

        }
    }
}
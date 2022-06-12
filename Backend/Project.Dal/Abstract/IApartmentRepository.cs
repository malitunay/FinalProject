using Project.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Dal.Abstract
{
    public interface IApartmentRepository : IGenericRepository<Apartment>
    {
        Apartment DeleteApartment(Apartment apartment);
        List<Apartment> GetApartmentsOfUser(int userId);
    }
}

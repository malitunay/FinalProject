using Microsoft.EntityFrameworkCore;
using Project.Dal.Abstract;
using Project.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Dal.Concrete.EntityFramework.Repository
{
    public class ApartmentRepository : GenericRepository<Apartment>, IApartmentRepository
    {
        public ApartmentRepository(DbContext context) : base(context)
        {

        }

        public Apartment DeleteApartment(Apartment apartment)
        {
            dbset.Update(apartment);
            return apartment;
        }


        public List<Apartment> GetApartmentsOfUser(int userId)
        {
            return dbset.Where(i=>i.UserId == userId).ToList();
        }
    }
}

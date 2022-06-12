using Project.Entity.Dto;
using Project.Entity.IBase;
using Project.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Interface
{
    public interface IApartmentService : IGenericService<Apartment, DtoApartment>
    {
        IResponse<DtoApartment> DeleteApartment(int userId, bool saveChanges = true);
        IResponse<DtoApartment> AssignApartmentToUser(DtoApartment entity, bool saveChanges = true);
    }
}

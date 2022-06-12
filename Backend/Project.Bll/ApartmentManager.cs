using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Project.Dal.Abstract;
using Project.Entity.Base;
using Project.Entity.Dto;
using Project.Entity.IBase;
using Project.Entity.Models;
using Project.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Bll
{
    public class ApartmentManager : GenericManager<Apartment, DtoApartment>, IApartmentService
    {
        public readonly IApartmentRepository _apartmentRepository;
        public readonly IUserRepository _userRepository;
        public ApartmentManager(IServiceProvider service, IUserRepository userRepository) : base(service)
        {
            _apartmentRepository = service.GetService<IApartmentRepository>();
            _userRepository = userRepository;
        }



        public IResponse<DtoApartment> DeleteApartment(int apartmentId, bool saveChanges = true)
        {
            try
            {
                var apartment = _apartmentRepository.Find(apartmentId);
                apartment.IsActive = false;
                var result = _apartmentRepository.DeleteApartment(apartment);

                if (saveChanges)
                    Save();

                return new Response<DtoApartment>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Daire silme işlemi başarılı",
                    Data = ObjectMapper.Mapper.Map<Apartment, DtoApartment>(result)
                };

            }
            catch (Exception ex)
            {
                return new Response<DtoApartment>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error:{ex.Message}",
                    Data = null
                };
            }
        }


        public IResponse<DtoApartment> AssignApartmentToUser(DtoApartment entity, bool saveChanges = true)
        {
            try
            {
                var apartment = _apartmentRepository.Find(entity.Id);
                apartment.UserId = entity.UserId;
                var user = _userRepository.Find(entity.UserId);
                var result = _apartmentRepository.DeleteApartment(apartment);

                if (saveChanges)
                    Save();

                return new Response<DtoApartment>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = $"{apartment.ApartmentBlockNo} bloktaki {apartment.ApartmentNo} nolu daire {user.Name} {user.Surname} kullanıcısına tahsis edilmiştir.",
                    Data = ObjectMapper.Mapper.Map<Apartment, DtoApartment>(result)
                };

            }
            catch (Exception ex)
            {
                return new Response<DtoApartment>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error:{ex.Message}",
                    Data = null
                };
            }
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Entity.Base;
using Project.Entity.Dto;
using Project.Entity.IBase;
using Project.Entity.Models;
using Project.Interface;
using Project.WebAPI.Base;
using System;
using System.Collections.Generic;

namespace Project.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentController : ApiBaseController<IApartmentService, Apartment, DtoApartment>
    {
        private readonly IApartmentService _apartmentService;
        public ApartmentController(IApartmentService apartmentService) : base(apartmentService)
        {
            _apartmentService = apartmentService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AddApartment")]
        public IResponse<DtoApartment> AddApartment(DtoApartment entity)
        {
            try
            {
                entity.ApartmentStatusId = 1;
                entity.UserId = 4;
                entity.IsActive = true;
                return _apartmentService.Add(entity);
            }
            catch (Exception ex)
            {
                return new Response<DtoApartment>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error : {ex.Message}",
                    Data = null
                };
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("UpdateApartment")]
        public IResponse<DtoApartment> UpdateApartment(DtoApartment entity)
        {
            try
            {
                return _apartmentService.Update(entity);
            }
            catch (Exception ex)
            {
                return new Response<DtoApartment>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error : {ex.Message}",
                    Data = null
                };
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("DeleteApartment")]
        public IResponse<DtoApartment> DeleteApartment(int apartmentId)
        {
            try
            {
                return _apartmentService.DeleteApartment(apartmentId);
            }
            catch (Exception ex)
            {
                return new Response<DtoApartment>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error : {ex.Message}",
                    Data = null
                };
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AssignApartmentToUser")]
        public IResponse<DtoApartment> AssignApartmentToUser(DtoApartment entity)
        {
            try
            {
                return _apartmentService.AssignApartmentToUser(entity);
            }
            catch (Exception ex)
            {
                return new Response<DtoApartment>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error : {ex.Message}",
                    Data = null
                };
            }
        }


        [HttpGet("GetAllActiveApartments")]
        public IResponse<List<DtoApartment>> GetAllActiveApartments()
        {
            try
            {
                return _apartmentService.GetAll(i => i.IsActive == true);
            }
            catch (Exception ex)
            {
                return new Response<List<DtoApartment>>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error : {ex.Message}",
                    Data = null
                };
            }
        }
    }
}

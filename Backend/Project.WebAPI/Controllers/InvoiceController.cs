using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Project.Entity.Base;
using Project.Entity.Dto;
using Project.Entity.IBase;
using Project.Entity.Models;
using Project.Interface;
using Project.WebAPI.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ApiBaseController<IInvoiceService, Invoice, DtoInvoice>
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IConfiguration _configuration;
        public InvoiceController(IInvoiceService invoiceService, IConfiguration configuraion) : base(invoiceService)
        {
            _invoiceService = invoiceService;
            _configuration = configuraion;
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("AddInvoiceToApartment")]
        public IResponse<DtoInvoice> AddInvoiceToApartment(DtoInvoice entity)
        {
            try
            {
                return _invoiceService.AddInvoiceToApartment(entity);
            }
            catch (Exception ex)
            {
                return new Response<DtoInvoice>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error : {ex.Message}",
                    Data = null
                };
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("AddInvoiceToAllApartments")]
        public IResponse<bool> AddInvoiceToAllApartments(DtoInvoice entity)
        {
            try
            {
                return _invoiceService.AddInvoiceToAllApartments(entity);
            }
            catch (Exception ex)
            {
                return new Response<bool>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error : {ex.Message}",
                    Data = false
                };
            }
        }



        [Authorize(Roles = "Admin")]
        [HttpGet("GetInvoicesByStatusId")]
        public IResponse<List<DtoInvoice>> GetInvoicesByStatusId(int invoiceStatusId) // invoiceStatusId : 1 ödenmedi, 2 ödendi
        {
            try
            {
                return _invoiceService.GetInvoicesByStatusId(invoiceStatusId);
            }
            catch (Exception ex)
            {
                return new Response<List<DtoInvoice>>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error : {ex.Message}",
                    Data = null
                };
            }
        }


        [Authorize(Roles = "Yönetici")]
        [HttpGet("GetInvoicesBySignedUserIdAndInvoiceStatusId")]
        public IResponse<List<DtoInvoice>> GetInvoicesBySignedUserIdAndInvoiceStatusId(int invoiceStatusId) // invoiceStatusId : 1 ödenmedi, 2 ödendi
        {
            try
            {
                var autUserId = Int32.Parse(User.Claims.First(i => i.Type == "jti").Value);
                return _invoiceService.GetInvoicesBySignedUserIdAndInvoiceStatusId(invoiceStatusId, autUserId);
            }
            catch (Exception ex)
            {
                return new Response<List<DtoInvoice>>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error : {ex.Message}",
                    Data = null
                };
            }
        }


        [Authorize(Roles = "Yönetici")]
        [HttpPost("PayInvoice")]
        public IResponse<DtoInvoice> PayInvoice(int invoiceId, CreditCard formCreditCardInfo)
        {
            try
            {
                var autUserId = Int32.Parse(User.Claims.First(i => i.Type == "jti").Value);
                return _invoiceService.PayInvoice(autUserId, invoiceId, formCreditCardInfo);
            }
            catch (Exception ex)
            {
                return new Response<DtoInvoice>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error : {ex.Message}",
                    Data = null
                };
            }
        }
    }
}

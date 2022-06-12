using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Payment.API.Services;
using Project.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Payment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IConfiguration _configuraion;
        public PaymentController(IConfiguration configuraion)
        {
            _configuraion = configuraion;
        }
        // GET: api/<PaymentController>
        [HttpGet("GetAllCreditCards")]
        public JsonResult GetAllCreditCards()
        {
            MongoClient dbset = new MongoClient(_configuraion.GetConnectionString("mongoDbConn"));
            var dbList = dbset.GetDatabase("PaymentDb").GetCollection<CreditCardMongo>("CreditCards").AsQueryable();
            return new JsonResult(dbList);
        }

        [HttpGet("GetByUserId")]
        public JsonResult GetByUserId(int userId)
        {
            MongoClient dbset = new MongoClient(_configuraion.GetConnectionString("mongoDbConn"));
            var creditCard = dbset.GetDatabase("PaymentDb").GetCollection<CreditCardMongo>("CreditCards").AsQueryable().Where(i => i.UserId == userId).FirstOrDefault();
            return new JsonResult(creditCard);
        }

        [HttpGet("GetBySignedUserId")]
        public JsonResult GetBySignedUserId()
        {
            var signedUserId = Int32.Parse(User.Claims.First(i => i.Type == "jti").Value);
            MongoClient dbset = new MongoClient(_configuraion.GetConnectionString("mongoDbConn"));
            var creditCard = dbset.GetDatabase("PaymentDb").GetCollection<CreditCardMongo>("CreditCards").AsQueryable().Where(i => i.UserId == signedUserId).FirstOrDefault();
            return new JsonResult(creditCard);
        }

        [HttpPost]
        public CreditCardMongo Add(CreditCardMongo creditCard)
        {
            MongoClient dbset = new MongoClient(_configuraion.GetConnectionString("mongoDbConn"));

            dbset.GetDatabase("PaymentDb").GetCollection<CreditCardMongo>("CreditCards").InsertOne(creditCard);

            return creditCard;
        }

        public class Request
        {
            public string autUserId { get; set; }
            public string invoiceId { get; set; }
            public string invoiceAmount { get; set; }
            public string UserId { get; set; }
            public string CreditCardNo { get; set; }
            public string CreditCardExpireMonth { get; set; }
            public string CreditCardExpireYear { get; set; }
            public string CreditCardCCV { get; set; }
            public string CreditCardBudget { get; set; }
        }

        public class Response<T>
        {
            public string Message { get; set; }
            public int StatusCode { get; set; }
            public T Data { get; set; }
        }

        [HttpPost("PayInvoice")]
        public Response<Request> PayInvoice([FromForm] Request content)
        {
            try
            {
                var autUserId = int.Parse(content.autUserId);
                var invoiceId = int.Parse(content.invoiceId);
                var invoiceAmount = decimal.Parse(content.invoiceAmount);
               // var UserId = int.Parse(content.UserId);
                MongoClient dbset = new MongoClient(_configuraion.GetConnectionString("mongoDbConn"));
                var creditCard = dbset.GetDatabase("PaymentDb").GetCollection<CreditCardMongo>("CreditCards").AsQueryable().Where(i => i.UserId == autUserId).FirstOrDefault();

                if (creditCard.CreditCardNo != content.CreditCardNo)
                {
                    return new Response<Request>
                    {
                        StatusCode = StatusCodes.Status500InternalServerError,
                        Message = "Girilen kredi kartı numarası hatalıdır. Lütfen kontrol ediniz.",
                        Data = null
                    };
                }
                if (creditCard.CreditCardExpireMonth != content.CreditCardExpireMonth || creditCard.CreditCardExpireYear != content.CreditCardExpireYear)
                {
                    return new Response<Request>
                    {
                        StatusCode = StatusCodes.Status500InternalServerError,
                        Message = "Girilen son kullanma tarihi hatalıdır. Lütfen kontrol ediniz.",
                        Data = null
                    };
                }
                if (creditCard.CreditCardCCV != content.CreditCardCCV)
                {
                    return new Response<Request>
                    {
                        StatusCode = StatusCodes.Status500InternalServerError,
                        Message = "Girilen CCV kodu hatalıdır. Lütfen kontrol ediniz.",
                        Data = null
                    };
                }
                if (decimal.Parse(creditCard.CreditCardBudget) < invoiceAmount)
                {
                    return new Response<Request>
                    {
                        StatusCode = StatusCodes.Status500InternalServerError,
                        Message = "Kredi kartı limitiniz yetersizdir.",
                        Data = null
                    };
                }

                var filter = Builders<CreditCardMongo>.Filter.Eq("UserId", autUserId);
                var willUpdateBudget = decimal.Parse(creditCard.CreditCardBudget) - invoiceAmount;
                var updateBudget = Builders<CreditCardMongo>.Update.Set("CreditCardBudget", willUpdateBudget);
                dbset.GetDatabase("PaymentDb").GetCollection<CreditCardMongo>("CreditCards").UpdateOne(filter, updateBudget);

                return new Response<Request>{
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Success",
                    Data = null
                };                
            }

            catch (Exception ex)
            {
                return new Response<Request>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error : {ex.Message}",
                    Data = null
                };
            }

        }

    }

}

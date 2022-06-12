using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//using MongoDB.Bson.IO;
//using MongoDB.Driver;
//using Project.API.Models;
using Project.Dal.Abstract;
using Project.Entity.Base;
using Project.Entity.Dto;
using Project.Entity.IBase;
using Project.Entity.Models;
using Project.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Project.Bll
{
    public class InvoiceManager : GenericManager<Invoice, DtoInvoice>, IInvoiceService
    {
        public readonly IInvoiceRepository _invoiceRepository;
        public readonly IApartmentRepository _apartmentRepository;
        private readonly IConfiguration _configuration;
        public InvoiceManager(IServiceProvider service, IConfiguration configuraion) : base(service)
        {
            _invoiceRepository = service.GetService<IInvoiceRepository>();
            _apartmentRepository = service.GetService<IApartmentRepository>();
            _configuration = configuraion;
        }

        public IResponse<DtoInvoice> AddInvoiceToApartment(DtoInvoice item, bool saveChanges = true)
        {
            try
            {
                item.InvoiceStatusId = 1;
                var model = ObjectMapper.Mapper.Map<Invoice>(item);
                var result = _invoiceRepository.Add(model);

                if (saveChanges)
                    Save();

                return new Response<DtoInvoice>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Success",
                    Data = ObjectMapper.Mapper.Map<Invoice, DtoInvoice>(result)
                };
            }
            catch (Exception ex)
            {
                return new Response<DtoInvoice>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error:{ex.Message}",
                    Data = null
                };
            }
        }

        public IResponse<bool> AddInvoiceToAllApartments(DtoInvoice item, bool saveChanges = true)
        {
            try
            {
                var inUseApartments = _apartmentRepository.GetAll(i => i.ApartmentStatusId == 1 && i.UserId != 4);

                foreach (var invoiceOfApartment in inUseApartments)
                {
                    item.InvoiceStatusId = 1;
                    item.ApartmentId = invoiceOfApartment.Id;
                    var model = ObjectMapper.Mapper.Map<Invoice>(item);
                    var result = _invoiceRepository.Add(model);
                }

                if (saveChanges)
                    Save();

                return new Response<bool>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Success",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new Response<bool>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error:{ex.Message}",
                    Data = false
                };
            }
        }

        public IResponse<List<DtoInvoice>> GetInvoicesByStatusId(int invoiceStatusId)
        {
            try
            {
                var list = _invoiceRepository.GetAll(i => i.InvoiceStatusId == invoiceStatusId);
                var listDto = list.Select(x => ObjectMapper.Mapper.Map<DtoInvoice>(x)).ToList();

                return new Response<List<DtoInvoice>>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Success",
                    Data = listDto
                };
            }
            catch (Exception ex)
            {
                return new Response<List<DtoInvoice>>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error:{ex.Message}",
                    Data = null
                };
            }
        }

        public IResponse<List<DtoInvoice>> GetInvoicesBySignedUserIdAndInvoiceStatusId(int invoiceStatusId, int autUserId)
        {
            try
            {
                var list = _invoiceRepository.GetInvoicesBySignedUserIdAndInvoiceStatusId(invoiceStatusId, autUserId);
                var listDto = list.Select(x => ObjectMapper.Mapper.Map<DtoInvoice>(x)).ToList();

                return new Response<List<DtoInvoice>>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Success",
                    Data = listDto
                };
            }
            catch (Exception ex)
            {
                return new Response<List<DtoInvoice>>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error:{ex.Message}",
                    Data = null
                };
            }
        }

        public IResponse<DtoInvoice> PayInvoice(int autUserId, int invoiceId, CreditCard formCreditCardInfo, bool saveChanges = true)
        {
            try
            {
                var invoice = _invoiceRepository.Find(invoiceId);

                if (invoice == null || invoice.InvoiceStatusId == 2)
                {
                    return new Response<DtoInvoice>
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Ödenmemmiş fatura/aidat bulunamadı.",
                        Data = null
                    };
                }
                else
                {
                    var payment = PostRequestAsync(autUserId, invoiceId, invoice.InvoiceAmount, formCreditCardInfo);
                    
                    JObject json = JObject.Parse(payment.Result);
                    foreach (var e in json)
                    {
                        Console.WriteLine(e);
                    }
                    if (payment.Result == "{\"message\":\"Success\",\"statusCode\":200,\"data\":null}")
                    {
                        var model = ObjectMapper.Mapper.Map<Invoice>(invoice);
                        var result = _invoiceRepository.PayInvoice(model, autUserId);

                        if (saveChanges)
                            Save();

                        return new Response<DtoInvoice>
                        {
                            StatusCode = StatusCodes.Status200OK,
                            Message = "Success",
                            Data = ObjectMapper.Mapper.Map<Invoice, DtoInvoice>(result)
                        };
                    }

                    else
                    {
                        return new Response<DtoInvoice>
                        {
                            StatusCode = StatusCodes.Status400BadRequest,
                            Message = payment.Result,
                            Data = null
                        };
                    }

                }



            }
            catch (Exception ex)
            {
                return new Response<DtoInvoice>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error:{ex.Message}",
                    Data = null
                };
            }
        }




        public async Task<string> PostRequestAsync(int autUserId, int invoiceId, decimal invoiceAmount, CreditCard formCreditCardInfo)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:57520");

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("autUserId",autUserId.ToString()),
                    new KeyValuePair<string, string>("invoiceId",invoiceId.ToString()),
                    new KeyValuePair<string, string>("invoiceAmount",invoiceAmount.ToString()),
                    new KeyValuePair<string, string>("UserId",formCreditCardInfo.UserId.ToString()),
                    new KeyValuePair<string, string>("creditCardNo", formCreditCardInfo.CreditCardNo),
                    new KeyValuePair<string, string>("CreditCardExpireMonth",formCreditCardInfo.CreditCardExpireMonth),
                    new KeyValuePair<string, string>("CreditCardExpireYear",formCreditCardInfo.CreditCardExpireYear),
                    new KeyValuePair<string, string>("CreditCardCCV",formCreditCardInfo.CreditCardCCV),
                    new KeyValuePair<string, string>("CreditCardBudget",formCreditCardInfo.CreditCardBudget)
                });


                var result = await client.PostAsync("/api/Payment/PayInvoice", content);




                string resultContent = await result.Content.ReadAsStringAsync();

                return new string(resultContent);


            }
        }
    }
}

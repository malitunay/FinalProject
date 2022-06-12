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
using System.Linq;

namespace Project.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ApiBaseController<IMessageService, Message, DtoMessage>
    {
        private readonly IMessageService _messageService;
        public MessageController(IMessageService service) : base(service)
        {
            _messageService = service;
        }

        [Authorize(Roles = "Yönetici")]
        [HttpPost("SendMessageToAdmin")]
        public IResponse<DtoMessage> SendMessageToAdmin(DtoMessage item)
        {
            try
            {
                var autUserId = Int32.Parse(User.Claims.First(i => i.Type == "jti").Value);
                return _messageService.SendMessageToAdmin(item, autUserId);
            }
            catch (System.Exception ex)
            {
                return new Response<DtoMessage>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error : {ex.Message}",
                    Data = null
                };
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("SendMessageToUser")]
        public IResponse<DtoMessage> SendMessageToUser(DtoMessage item)
        {
            try
            {
                var autUserId = Int32.Parse(User.Claims.First(i => i.Type == "jti").Value);
                return _messageService.SendMessageToUser(item, autUserId);
            }
            catch (System.Exception ex)
            {
                return new Response<DtoMessage>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error : {ex.Message}",
                    Data = null
                };
            }
        }


        [Authorize(Roles = "Admin, Yönetici")]
        [HttpGet("GetMessagesByUserId")]
        public IResponse<List<DtoMessage>> GetMessagesByUserId(int userId)
        {
            try
            {
                return _messageService.GetMessagesByUserId(userId);
            }
            catch (System.Exception ex)
            {
                return new Response<List<DtoMessage>>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error : {ex.Message}",
                    Data = null
                };
            }
        }

        [Authorize(Roles = "Yönetici")]
        [HttpGet("GetMessagesBySigninUserId")]
        public IResponse<List<DtoMessage>> GetMessagesBySigninUserId()
        {
            try
            {
                var autUserId = Int32.Parse(User.Claims.First(i => i.Type == "jti").Value);
                return _messageService.GetMessagesByUserId(autUserId);
            }
            catch (System.Exception ex)
            {
                return new Response<List<DtoMessage>>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error : {ex.Message}",
                    Data = null
                };
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("GetMessagesAndUsers")]
        public IResponse<List<DtoUnreadMessageCount>> GetMessagesAndUsers()
        {
            try
            {
                return _messageService.GetMessagesAndUsers();
            }
            catch (System.Exception ex)
            {
                return new Response<List<DtoUnreadMessageCount>>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error : {ex.Message}",
                    Data = null
                };
            }
        }



        [Authorize(Roles = "Admin, Yönetici")]
        [HttpPost("MarkRead")]
        public IResponse<bool> MarkRead(int relatedUserId)
        {
            try
            {
                var autUserId = Int32.Parse(User.Claims.First(i => i.Type == "jti").Value);
                return _messageService.MarkRead(relatedUserId, autUserId);
            }
            catch (System.Exception ex)
            {
                return new Response<bool>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error : {ex.Message}",
                    Data = false
                };
            }
        }

    }
}

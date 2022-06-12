using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
    public class MessageManager : GenericManager<Message, DtoMessage>, IMessageService
    {
        public readonly IMessageRepository messageRepository;
        public readonly IUserRepository _userRepository;
        protected DbContext _context;
        protected DbSet<DtoUnreadMessageCount> dbset;
        public MessageManager(IServiceProvider service, DbContext context) : base(service)
        {
            messageRepository = service.GetService<IMessageRepository>();
            _userRepository = service.GetService<IUserRepository>();
            _context = context;
            this.dbset = _context.Set<DtoUnreadMessageCount>();
        }

        public IResponse<List<DtoMessage>> GetMessagesByUserId(int userId, bool saveChanges = true)
        {
            try
            {
                var messages = messageRepository.GetAll(i => i.RelatedUserId == userId);
                var listDto = messages.Select(x => ObjectMapper.Mapper.Map<DtoMessage>(x)).ToList();

                return new Response<List<DtoMessage>>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Success",
                    Data = listDto
                };
            }
            catch (Exception ex)
            {
                return new Response<List<DtoMessage>>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error:{ex.Message}",
                    Data = null
                };
            }
        }

        public IResponse<DtoMessage> SendMessageToAdmin(DtoMessage message, int autUserId, bool saveChanges = true)
        {
            try
            {

                message.SenderId = autUserId;
                message.RelatedUserId = autUserId;
                message.ReceiverId = 1;  // admin
                message.MessageStatusId = 1;  // mesaj okunmadı
                message.Time = DateTime.Now;

                var model = ObjectMapper.Mapper.Map<Message>(message);
                messageRepository.SendMessage(model);

                if (saveChanges)
                    Save();

                return new Response<DtoMessage>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Success",
                    Data = message
                };


            }
            catch (Exception ex)
            {
                return new Response<DtoMessage>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error:{ex.Message}",
                    Data = null
                };
            }
        }


        public IResponse<DtoMessage> SendMessageToUser(DtoMessage message, int autUserId, bool saveChanges = true)
        {
            try
            {
                if (autUserId == 1)
                {
                    message.SenderId = autUserId;
                    message.RelatedUserId = message.ReceiverId;
                    message.MessageStatusId = 1;  // mesaj okunmadı
                    message.Time = DateTime.Now;

                    var model = ObjectMapper.Mapper.Map<Message>(message);
                    messageRepository.SendMessage(model);

                    if (saveChanges)
                        Save();

                    return new Response<DtoMessage>
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Success",
                        Data = message
                    };
                }

                else
                {
                    return new Response<DtoMessage>
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Admin olmadığınız için kullanıcıya mesaj gönderemezsiniz",
                        Data = null
                    };
                }

            }
            catch (Exception ex)
            {
                return new Response<DtoMessage>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error:{ex.Message}",
                    Data = null
                };
            }
        }


        public IResponse<List<DtoUnreadMessageCount>> GetMessagesAndUsers()
        {
            try
            {
                var result = messageRepository.RawSqlQuery("select MIN(u.Id) as RelatedUserId,"
                    + "MIN(u.Name) Name, MIN(u.Surname) Surname, ISNULL(COUNT(m.MessageStatusId), 0)"
                    + "MessageCount from dbo.Users as u LEFT OUTER JOIN dbo.Messages as m on u.Id = m.RelatedUserId and m.MessageStatusId = 1 GROUP BY u.Id",
                    x => new DtoUnreadMessageCount { 
                        RelatedUserId = (int)x[0], 
                        Name = (string)x[1], 
                        Surname = (string)x[2], 
                        MessageCount = (int)x[3] 
                    });


                return new Response<List<DtoUnreadMessageCount>>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new Response<List<DtoUnreadMessageCount>>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error:{ex.Message}",
                    Data = null
                };
            }
        }


        public IResponse<bool> MarkRead(int relatedUserId, int autUserId, bool saveChanges = true)
        {
            try
            {
                if (autUserId == relatedUserId || autUserId == 1)
                {
                    var unreadMessages = messageRepository.GetAll(i => i.MessageStatusId == 1 && i.ReceiverId == autUserId && i.RelatedUserId == relatedUserId);

                    foreach (var item in unreadMessages)
                    {
                        item.MessageStatusId = 2;
                        var model = ObjectMapper.Mapper.Map<Message>(item);
                        var result = messageRepository.Update(model);
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

                else
                {
                    return new Response<bool>
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Can not mark as read",
                        Data = false
                    };
                }
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
    }
}

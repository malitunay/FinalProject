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
    public interface IMessageService : IGenericService<Message, DtoMessage>
    {
        IResponse<DtoMessage> SendMessageToAdmin(DtoMessage message, int autUserId, bool saveChanges = true);
        IResponse<DtoMessage> SendMessageToUser(DtoMessage message, int autUserId, bool saveChanges = true);
        IResponse<List<DtoMessage>> GetMessagesByUserId(int userId, bool saveChanges = true);
        IResponse<bool> MarkRead(int relatedUserId, int autUserId, bool saveChanges = true);
        IResponse<List<DtoUnreadMessageCount>> GetMessagesAndUsers();
    }
}

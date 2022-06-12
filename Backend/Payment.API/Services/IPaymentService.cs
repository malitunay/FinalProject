using Project.API.Models;
using System.Collections.Generic;

namespace Payment.API.Services
{
    public interface IPaymentService
    {
        List<CreditCardMongo> GetAll();
        CreditCardMongo Get(string id);
        CreditCardMongo Create(CreditCardMongo credit);
        void Update(string id, CreditCardMongo credit);
        void Remove(string id);



    }
}

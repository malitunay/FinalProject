//using MongoDB.Driver;
//using Payment.API.Models;
//using Project.API.Models;
//using System.Collections.Generic;

//namespace Payment.API.Services
//{
//    public class PaymentService : IPaymentService
//    {
//        private readonly IMongoCollection<CreditCard> _paymentCollection;
//        public PaymentService(IPaymentServiceSettings settings, IMongoClient mongoClient)
//        {
//            var dbset = mongoClient.GetDatabase(settings.DatabaseName);
//            _paymentCollection = dbset.GetCollection<CreditCard>(settings.PaymentCollectionName);
//        }
//        public CreditCard Create(CreditCard creditCard)
//        {
//            _paymentCollection.InsertOne(creditCard);
//            return creditCard;
//        }

//        public CreditCard Get(string id)
//        {
//            return _paymentCollection.Find(creditCard => creditCard.Id == id).FirstOrDefault();
//        }

//        public List<CreditCard> GetAll()
//        {
//            return _paymentCollection.Find(creditCard => true).ToList();
//        }

//        public void Remove(string id)
//        {
//            _paymentCollection.DeleteOne(creditCard => creditCard.Id == id);
//        }

//        public void Update(string id, CreditCard creditCard)
//        {
//            _paymentCollection.ReplaceOne(creditCard => creditCard.Id == id, creditCard);
//        }
//    }
//}

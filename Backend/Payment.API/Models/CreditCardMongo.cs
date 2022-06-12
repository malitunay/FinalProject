using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.API.Models
{
    [BsonIgnoreExtraElements]
    public class CreditCardMongo
    {
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        //[BsonElement("userId")]
        public int UserId { get; set; }
        public string CreditCardNo { get; set; }
        public string CreditCardExpireMonth { get; set; }
        public string CreditCardExpireYear { get; set; }
        public string CreditCardCCV { get; set; }
        public string CreditCardBudget { get; set; }
    }
}

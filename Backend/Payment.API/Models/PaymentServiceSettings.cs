using System;

namespace Payment.API.Models
{
    public class PaymentServiceSettings : IPaymentServiceSettings
    {
        public string PaymentCollectionName { get; set; } = String.Empty;
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
    }
}

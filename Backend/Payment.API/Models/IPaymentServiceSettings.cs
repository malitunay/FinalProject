namespace Payment.API.Models
{
    public interface IPaymentServiceSettings
    {
        string PaymentCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}

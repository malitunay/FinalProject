using Project.Entity.Base;
using System;
using System.Collections.Generic;

#nullable disable

namespace Project.Entity.Dto
{
    public partial class DtoInvoice : DtoBase
    {
        public int Id { get; set; }
        public int InvoicePeriod { get; set; }
        public int InvoiceTypeId { get; set; }
        public int InvoiceStatusId { get; set; }
        public decimal InvoiceAmount { get; set; }
        public int ApartmentId { get; set; }
        public DateTime PaymentDateTime { get; set; }
    }
}

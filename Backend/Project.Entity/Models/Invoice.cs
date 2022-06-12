using Project.Entity.Base;
using System;
using System.Collections.Generic;

#nullable disable

namespace Project.Entity.Models
{
    public partial class Invoice : EntityBase
    {
        public int Id { get; set; }
        public int InvoicePeriod { get; set; }
        public int InvoiceTypeId { get; set; }
        public int InvoiceStatusId { get; set; }
        public decimal InvoiceAmount { get; set; }
        public int ApartmentId { get; set; }
        public DateTime PaymentDateTime { get; set; }

        public virtual Apartment Apartment { get; set; }
        public virtual InvoiceStatus InvoiceStatus { get; set; }
        public virtual InvoiceType InvoiceType { get; set; }
    }
}

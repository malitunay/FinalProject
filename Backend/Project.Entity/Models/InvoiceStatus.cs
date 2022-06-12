using Project.Entity.Base;
using System;
using System.Collections.Generic;

#nullable disable

namespace Project.Entity.Models
{
    public partial class InvoiceStatus : EntityBase
    {
        public InvoiceStatus()
        {
            Invoices = new HashSet<Invoice>();
        }

        public int Id { get; set; }
        public string InvoiceStatus1 { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}

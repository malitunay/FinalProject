using Project.Entity.Base;
using System;
using System.Collections.Generic;

#nullable disable

namespace Project.Entity.Models
{
    public partial class InvoiceType : EntityBase
    {
        public InvoiceType()
        {
            Invoices = new HashSet<Invoice>();
        }

        public int Id { get; set; }
        public string InvoiceTypes { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}

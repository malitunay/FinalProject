using Project.Entity.Base;
using System;
using System.Collections.Generic;

#nullable disable

namespace Project.Entity.Dto
{
    public partial class DtoInvoiceStatus : DtoBase
    {

        public int Id { get; set; }
        public string InvoiceStatus { get; set; }

    }
}

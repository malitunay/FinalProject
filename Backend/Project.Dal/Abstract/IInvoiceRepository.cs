using Project.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Dal.Abstract
{
    public interface IInvoiceRepository : IGenericRepository<Invoice>
    {
        List<Invoice> GetPaidInvoices();
        List<Invoice> GetInvoicesBySignedUserIdAndInvoiceStatusId(int invoiceStatusId, int autUserId);
        Invoice PayInvoice(Invoice invoice, int autUserId);
    }
}

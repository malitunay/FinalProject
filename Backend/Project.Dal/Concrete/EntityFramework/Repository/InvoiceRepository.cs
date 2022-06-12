using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Project.Dal.Abstract;
using Project.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Dal.Concrete.EntityFramework.Repository
{
    public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
    {
        
        public InvoiceRepository(DbContext context) : base(context)
        {
           
        }

        public List<Invoice> GetPaidInvoices()
        {
            return dbset.Where(i=>i.InvoiceStatusId == 2).ToList();
        }

        public Invoice PayInvoice(Invoice invoice, int autUserId)
        {
            var invoiceData = dbset.Find(invoice.Id);
            invoiceData.InvoiceStatusId = 2;
            return Update(invoiceData);
        }

        public List<Invoice> GetInvoicesBySignedUserIdAndInvoiceStatusId(int invoiceStatusId, int autUserId)
        {
            return dbset.FromSqlInterpolated($"select invoice.Id, invoice.InvoicePeriod,invoice.InvoiceTypeId, invoice.InvoiceStatusId, invoice.InvoiceAmount, invoice.ApartmentId, invoice.PaymentDateTime from dbo.Invoices as invoice inner join dbo.Apartments as apartment on invoice.ApartmentId = apartment.Id where UserId = {autUserId} and InvoiceStatusId = {invoiceStatusId}").ToList();
        }


        
    }
}

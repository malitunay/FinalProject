//using Project.API.Models;
using Project.Entity.Dto;
using Project.Entity.IBase;
using Project.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Interface
{
    public interface IInvoiceService : IGenericService<Invoice, DtoInvoice>
    {
        IResponse<List<DtoInvoice>> GetInvoicesByStatusId(int invoiceStatus);
        IResponse<List<DtoInvoice>> GetInvoicesBySignedUserIdAndInvoiceStatusId(int invoiceStatus, int autUserId);
        IResponse<DtoInvoice> AddInvoiceToApartment(DtoInvoice invoice, bool saveChanges = true);
        IResponse<DtoInvoice> PayInvoice(int autUserId, int invoiceId, CreditCard formCreditCardInfo, bool saveChanges = true);
        IResponse<bool> AddInvoiceToAllApartments(DtoInvoice invoice, bool saveChanges = true);
    }
}

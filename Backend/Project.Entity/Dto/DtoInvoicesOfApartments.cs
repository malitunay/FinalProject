using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entity.Dto
{
    public class DtoInvoicesOfApartments
    {
        public DtoInvoice Invoice { get; set; }
        public DtoApartment Apartment { get; set; }
    }
}

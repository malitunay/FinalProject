using Project.Entity.Base;
using System;
using System.Collections.Generic;

#nullable disable

namespace Project.Entity.Models
{
    public partial class Apartment : EntityBase
    {
        public Apartment()
        {
            Invoices = new HashSet<Invoice>();
        }

        public int Id { get; set; }
        public string ApartmentBlockNo { get; set; }
        public int ApartmentStatusId { get; set; }
        public string ApartmentType { get; set; }
        public int ApartmentFloor { get; set; }
        public int ApartmentNo { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }

        public virtual ApartmentStatus ApartmentStatus { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}

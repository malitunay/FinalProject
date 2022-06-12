using Project.Entity.Base;
using System;
using System.Collections.Generic;

#nullable disable

namespace Project.Entity.Models
{
    public partial class ApartmentStatus : EntityBase
    {
        public ApartmentStatus()
        {
            Apartments = new HashSet<Apartment>();
        }

        public int Id { get; set; }
        public string ApartmentStatus1 { get; set; }

        public virtual ICollection<Apartment> Apartments { get; set; }
    }
}

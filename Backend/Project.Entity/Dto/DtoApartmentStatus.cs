using Project.Entity.Base;
using System;
using System.Collections.Generic;

#nullable disable

namespace Project.Entity.Dto
{
    public partial class DtoApartmentStatus : DtoBase
    {

        public int Id { get; set; }
        public string ApartmentBlockNo { get; set; }
        public int ApartmentStatusId { get; set; }
        public string ApartmentType { get; set; }
        public int ApartmentFloor { get; set; }
        public int ApartmentNo { get; set; }
        public int UserId { get; set; }

    }
}

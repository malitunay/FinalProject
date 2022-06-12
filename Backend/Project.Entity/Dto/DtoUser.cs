using Project.Entity.Base;
using System;
using System.Collections.Generic;

#nullable disable

namespace Project.Entity.Dto
{
    public partial class DtoUser : DtoBase
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int UserTypeId { get; set; }
        public string Password { get; set; }
        public string Tcnumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Plate { get; set; }
        public int RolId { get; set; }
        public bool IsActive { get; set; }

    }
}

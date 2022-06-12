using Project.Entity.Base;
using System;
using System.Collections.Generic;

#nullable disable

namespace Project.Entity.Dto
{
    public partial class DtoUserType : DtoBase
    {
        public int Id { get; set; }
        public string UserTypes { get; set; }

    }
}

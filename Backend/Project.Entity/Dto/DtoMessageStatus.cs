using Project.Entity.Base;
using System;
using System.Collections.Generic;

#nullable disable

namespace Project.Entity.Dto
{
    public partial class DtoMessageStatus : DtoBase
    {
        public int Id { get; set; }
        public string MessageStatus1 { get; set; }
    }
}

using Project.Entity.Base;
using System;
using System.Collections.Generic;

#nullable disable

namespace Project.Entity.Dto
{
    public partial class DtoUnreadMessageCount : DtoBase
    {
        public int RelatedUserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int MessageCount { get; set; }
    }
}

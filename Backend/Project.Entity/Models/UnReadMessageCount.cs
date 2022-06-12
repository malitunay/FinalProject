using Project.Entity.Base;
using System;
using System.Collections.Generic;

#nullable disable

namespace Project.Entity.Models
{
    public partial class UnReadMessageCount : EntityBase
    {
        public int RelatedUserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int MessageCount { get; set; }
    }
}

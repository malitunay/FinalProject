using Project.Entity.Base;
using System;
using System.Collections.Generic;

#nullable disable

namespace Project.Entity.Dto
{
    public partial class DtoMessage : DtoBase
    {
        public int Id { get; set; }
        public int RelatedUserId { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public int MessageStatusId { get; set; }
        public DateTime Time { get; set; }
        public string MessageText { get; set; }
    }
}

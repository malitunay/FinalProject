using Project.Entity.Base;
using System;
using System.Collections.Generic;

#nullable disable

namespace Project.Entity.Models
{
    public partial class MessageStatus : EntityBase
    {
        public MessageStatus()
        {
            Messages = new HashSet<Message>();
        }

        public int Id { get; set; }
        public string MessageStatu { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}

using Project.Entity.Base;
using System;
using System.Collections.Generic;

#nullable disable

namespace Project.Entity.Models
{
    public partial class UserType : EntityBase
    {
        public UserType()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string UserTypes { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}

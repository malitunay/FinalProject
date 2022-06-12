using Project.Entity.Base;
using System;
using System.Collections.Generic;

#nullable disable

namespace Project.Entity.Models
{
    public partial class User : EntityBase
    {
        public User()
        {
            Apartments = new HashSet<Apartment>();
            MessageReceivers = new HashSet<Message>();
            MessageSenders = new HashSet<Message>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public int UserTypeId { get; set; }
        public string Password { get; set; }
        public string Tcnumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Plate { get; set; }
        public int RolId { get; set; }
        public bool IsActive { get; set; }

        public virtual Role Rol { get; set; }
        public virtual UserType UserType { get; set; }
        public virtual ICollection<Apartment> Apartments { get; set; }
        public virtual ICollection<Message> MessageReceivers { get; set; }
        public virtual ICollection<Message> MessageSenders { get; set; }
    }
}

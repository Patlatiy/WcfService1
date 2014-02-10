using System;
using System.Collections.Generic;

namespace WcfService1.Models
{
    public partial class User
    {
        public User()
        {
            this.Orders = new List<Order>();
        }

        public int ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public byte RoleID { get; set; }
        public System.DateTime RegistrationDateTime { get; set; }
        public Nullable<System.DateTime> LastActiveDateTime { get; set; }
        public bool IsBlocked { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual UserRole UserRole { get; set; }
    }
}

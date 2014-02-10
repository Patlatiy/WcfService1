//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebStore
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public User()
        {
            this.Orders = new HashSet<Order>();
        }
    
        public int ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public byte RoleId { get; set; }
        public System.DateTime RegistrationDateTime { get; set; }
        public Nullable<System.DateTime> LastActiveDateTime { get; set; }
        public bool IsBlocked { get; set; }
    
        public virtual ICollection<Order> Orders { get; set; }
        public virtual UserRole UserRole { get; set; }
    }
}

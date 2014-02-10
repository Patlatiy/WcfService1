using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebStore.PocoModels
{
    public class User
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte RoleId { get; set; }
        public DateTime RegistrationDateTime { get; set; }
        public DateTime? LastActiveDateTime { get; set; }
        public bool IsBlocked { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual UserRole UserRole { get; set; }

        public User()
        {

        }
    }
}
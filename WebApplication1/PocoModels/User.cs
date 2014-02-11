using System;
using System.Collections.Generic;

namespace WebStore.PocoModels
{
    public class User
    {
        public User()
        {
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
        public byte RoleId { get; set; }
        public DateTime RegistrationDateTime { get; set; }
        public DateTime? LastActiveDateTime { get; set; }
        public bool IsBlocked { get; set; }

        public ICollection<Order> Orders { get; set; }
        //public UserRole UserRole { get; set; }
    }
}
using System;
using System.Collections.Generic;

namespace WcfService1.Models
{
    public partial class UserRole
    {
        public UserRole()
        {
            this.Users = new List<User>();
        }

        public byte ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}

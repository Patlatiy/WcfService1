using System;
using System.Collections.Generic;

namespace WcfService1.Models
{
    public partial class OrderState
    {
        public OrderState()
        {
            this.Orders = new List<Order>();
        }

        public byte ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}

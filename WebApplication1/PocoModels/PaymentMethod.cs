using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebStore.PocoModels
{
    public class PaymentMethod
    {
        public PaymentMethod()
        {
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
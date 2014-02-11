using System.Collections.Generic;

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
using System.Collections.Generic;

namespace WebStore.PocoModels
{
    public class OrderState
    {
        public OrderState()
        {
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
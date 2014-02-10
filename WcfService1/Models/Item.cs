using System;
using System.Collections.Generic;

namespace WcfService1.Models
{
    public partial class Item
    {
        public Item()
        {
            this.OrderPositions = new List<OrderPosition>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public virtual ItemCategory ItemCategory { get; set; }
        public virtual ICollection<OrderPosition> OrderPositions { get; set; }
    }
}

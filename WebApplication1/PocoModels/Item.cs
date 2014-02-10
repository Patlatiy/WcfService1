using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebStore.PocoModels
{
    public class Item
    {
        public Item()
        {
        }

        public string Name { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }

        public ItemCategory ItemCategory { get; set; }
        public ICollection<OrderPosition> OrderPositions { get; set; }
    }
}
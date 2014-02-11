using System;
using System.Collections.Generic;

namespace WebStore.PocoModels
{
    public class Item
    {
        public Item()
        {
        }

        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }

        public ItemCategory ItemCategory { get; set; }
        public ICollection<OrderPosition> OrderPositions { get; set; }
    }
}
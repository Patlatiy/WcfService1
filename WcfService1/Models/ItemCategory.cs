using System;
using System.Collections.Generic;

namespace WcfService1.Models
{
    public partial class ItemCategory
    {
        public ItemCategory()
        {
            this.Items = new List<Item>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}

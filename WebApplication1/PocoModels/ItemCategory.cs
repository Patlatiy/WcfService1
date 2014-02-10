using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebStore.PocoModels
{
    public class ItemCategory
    {
        public ItemCategory()
        {
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebStore.PocoModels
{
    public class OrderPosition
    {

        public OrderPosition()
        {
        }

        public int OrderID { get; set; }
        public int ItemID { get; set; }
        public int ItemQuantity { get; set; }

        public Item Item { get; set; }
        public Order Order { get; set; }
    }
}
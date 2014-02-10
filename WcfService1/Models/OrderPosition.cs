using System;
using System.Collections.Generic;

namespace WcfService1.Models
{
    public partial class OrderPosition
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int ItemID { get; set; }
        public int ItemQuantity { get; set; }
        public virtual Item Item { get; set; }
        public virtual Order Order { get; set; }
    }
}

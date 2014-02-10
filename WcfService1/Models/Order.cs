using System;
using System.Collections.Generic;

namespace WcfService1.Models
{
    public partial class Order
    {
        public Order()
        {
            this.OrderPositions = new List<OrderPosition>();
        }

        public int ID { get; set; }
        public int UserID { get; set; }
        public Nullable<System.DateTime> DateIssued { get; set; }
        public Nullable<System.DateTime> DateEnded { get; set; }
        public byte StateID { get; set; }
        public Nullable<byte> PaymentMethodID { get; set; }
        public virtual OrderState OrderState { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<OrderPosition> OrderPositions { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebStore.PocoModels
{
    public class Order
    {
        public Order()
        {
        }

        public int UserID { get; set; }
        public Nullable<System.DateTime> DateIssued { get; set; }
        public Nullable<System.DateTime> DateEnded { get; set; }
        public byte StateID { get; set; }
        public Nullable<byte> PaymentMethodID { get; set; }

        public OrderState OrderState { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public User User { get; set; }
        public ICollection<OrderPosition> OrderPositions { get; set; }
    }
}
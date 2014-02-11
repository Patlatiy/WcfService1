using System;
using System.Collections.Generic;

namespace WebStore.PocoModels
{
    public class Order
    {
        public Order()
        {
        }

        public int UserId { get; set; }
        public DateTime? DateIssued { get; set; }
        public DateTime? DateEnded { get; set; }
        public byte StateId { get; set; }
        public byte? PaymentMethodId { get; set; }

        public OrderState OrderState { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public User User { get; set; }
        public ICollection<OrderPosition> OrderPositions { get; set; }
    }
}
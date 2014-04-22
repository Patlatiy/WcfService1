//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebStore.App_Data.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public Order()
        {
            this.Total = 0m;
            this.OrderPositions = new HashSet<OrderPosition>();
        }
    
        public int ID { get; set; }
        public int UserID { get; set; }
        public Nullable<System.DateTime> DateIssued { get; set; }
        public Nullable<System.DateTime> DateEnded { get; set; }
        public byte StateID { get; set; }
        public Nullable<byte> PaymentMethodID { get; set; }
        public string DeliveryAddress { get; set; }
        public string Comment { get; set; }
        public string Login { get; set; }
        public string StateName { get; set; }
        public decimal Total { get; set; }
    
        public virtual OrderState OrderState { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<OrderPosition> OrderPositions { get; set; }
    }
}

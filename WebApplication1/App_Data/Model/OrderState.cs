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
    
    public partial class OrderState
    {
        public OrderState()
        {
            this.Orders = new HashSet<Order>();
        }
    
        public byte ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<Order> Orders { get; set; }
    }
}

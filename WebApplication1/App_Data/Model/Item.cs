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
    
    public partial class Item
    {
        public Item()
        {
            this.OrderPositions = new HashSet<OrderPosition>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
    
        public virtual ItemCategory ItemCategory { get; set; }
        public virtual ICollection<OrderPosition> OrderPositions { get; set; }
    }
}

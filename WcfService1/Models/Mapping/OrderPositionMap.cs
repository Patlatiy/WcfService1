using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WcfService1.Models.Mapping
{
    public class OrderPositionMap : EntityTypeConfiguration<OrderPosition>
    {
        public OrderPositionMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            this.ToTable("OrderPosition", "WS");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.OrderID).HasColumnName("OrderID");
            this.Property(t => t.ItemID).HasColumnName("ItemID");
            this.Property(t => t.ItemQuantity).HasColumnName("ItemQuantity");

            // Relationships
            this.HasRequired(t => t.Item)
                .WithMany(t => t.OrderPositions)
                .HasForeignKey(d => d.ItemID);
            this.HasRequired(t => t.Order)
                .WithMany(t => t.OrderPositions)
                .HasForeignKey(d => d.OrderID);

        }
    }
}

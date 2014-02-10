using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WcfService1.Models.Mapping
{
    public class OrderMap : EntityTypeConfiguration<Order>
    {
        public OrderMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            this.ToTable("Order", "WS");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.DateIssued).HasColumnName("DateIssued");
            this.Property(t => t.DateEnded).HasColumnName("DateEnded");
            this.Property(t => t.StateID).HasColumnName("StateID");
            this.Property(t => t.PaymentMethodID).HasColumnName("PaymentMethodID");

            // Relationships
            this.HasRequired(t => t.OrderState)
                .WithMany(t => t.Orders)
                .HasForeignKey(d => d.StateID);
            this.HasOptional(t => t.PaymentMethod)
                .WithMany(t => t.Orders)
                .HasForeignKey(d => d.PaymentMethodID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.Orders)
                .HasForeignKey(d => d.UserID);

        }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WcfService1.Models.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Password)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Surname)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Patronymic)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("User", "WS");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Surname).HasColumnName("Surname");
            this.Property(t => t.Patronymic).HasColumnName("Patronymic");
            this.Property(t => t.RoleID).HasColumnName("RoleID");
            this.Property(t => t.RegistrationDateTime).HasColumnName("RegistrationDateTime");
            this.Property(t => t.LastActiveDateTime).HasColumnName("LastActiveDateTime");
            this.Property(t => t.IsBlocked).HasColumnName("IsBlocked");

            // Relationships
            this.HasRequired(t => t.UserRole)
                .WithMany(t => t.Users)
                .HasForeignKey(d => d.RoleID);

        }
    }
}

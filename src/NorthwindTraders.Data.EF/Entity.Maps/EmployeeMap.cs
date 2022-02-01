using System.Data.Entity.ModelConfiguration;
using NorthwindTraders.Domain.Entities;

namespace NorthwindTraders.Data.EF.Entity.Maps
{
    internal class EmployeeMap : EntityTypeConfiguration<Employee>
    {
        internal EmployeeMap()
        {
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("EmployeeID");

            Property(x => x.SupervisorId)
                .HasColumnName("ReportsTo");

            Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(20);

            Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(10);

            Property(x => x.Title)
                .HasMaxLength(30);

            Property(x => x.TitleOfCourtesy)
                .HasMaxLength(25);

            Property(x => x.Address.Street)
                .HasColumnName("Address")
                .HasMaxLength(60);

            Property(x => x.Address.City)
                .HasColumnName("City")
                .HasMaxLength(15);

            Property(x => x.Address.Region)
                .HasColumnName("Region")
                .HasMaxLength(15);

            Property(x => x.Address.ZipCode)
                .HasColumnName("PostalCode")
                .HasMaxLength(10);

            Property(x => x.Address.Country)
                .HasColumnName("Country")
                .HasMaxLength(15);

            Property(x => x.HomePhone)
                .HasMaxLength(24);

            Property(x => x.Extension)
                .HasMaxLength(4);

            Property(x => x.Photo)
                .HasColumnType("image");

            Property(x => x.Notes)
                .HasColumnType("ntext");

            Property(x => x.PhotoPath)
                .HasMaxLength(255);

            HasMany(e => e.Subordinates)
                .WithOptional(e => e.Supervisor)
                .HasForeignKey(e => e.SupervisorId);

            //HasMany(e => e.Territories)
            //    .WithMany(e => e.Employees)
            //    .Map(m => m.ToTable("EmployeeTerritories").MapLeftKey("EmployeeID").MapRightKey("TerritoryID"));
        }
    }
}
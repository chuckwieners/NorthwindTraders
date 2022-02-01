using System.Data.Entity.ModelConfiguration;
using NorthwindTraders.Domain.Entities;

namespace NorthwindTraders.Data.EF.Entity.Maps
{
    internal class SupplierMap : EntityTypeConfiguration<Supplier>
    {
        internal SupplierMap()
        {
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("SupplierID");

            Property(x => x.Name)
                .HasColumnName("CompanyName")
                .IsRequired()
                .HasMaxLength(40);

            Property(x => x.ContactName)
                .HasMaxLength(30);

            Property(x => x.ContactTitle)
                .HasMaxLength(30);

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

            Property(x => x.Phone)
                .HasMaxLength(24);

            Property(x => x.Fax)
                .HasMaxLength(24);

            Property(x => x.HomePage)
                .HasColumnType("ntext");
        }
    }
}
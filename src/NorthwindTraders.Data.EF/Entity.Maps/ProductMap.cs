using System.Data.Entity.ModelConfiguration;
using NorthwindTraders.Domain.Entities;

namespace NorthwindTraders.Data.EF.Entity.Maps
{
    internal class ProductMap : EntityTypeConfiguration<Product>
    {
        internal ProductMap()
        {
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("ProductID");

            Property(x => x.Name)
                .HasColumnName("ProductName")
                .IsRequired()
                .HasMaxLength(40);

            Property(x => x.QuantityPerUnit)
                .HasMaxLength(20);

            Property(x => x.Price)
                .HasColumnName("UnitPrice")//Resolved the name for the field so that our test could run
                .HasColumnType("money")
                .HasPrecision(19, 4);
            //Added the IsDiscontinued property and Resolved the name for the 
            //field so that our test could run
            Property(x => x.IsDiscontinued)
                .HasColumnName("Discontinued");


            //HasMany(e => e.OrderDetails)
            //    .WithRequired(e => e.Product)
            //    .WillCascadeOnDelete(false);
        }
    }
}
using System.Data.Entity.ModelConfiguration;
using NorthwindTraders.Domain.Entities;

namespace NorthwindTraders.Data.EF.Entity.Maps
{
    internal class CategoryMap : EntityTypeConfiguration<Category>
    {
        internal CategoryMap()
        {
            //NOTE: Since the table is the same name as the entity
            //there is no need to specify the table name
            //ToTable("Category");

            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("CategoryID");

            Property(x => x.Name)
                .HasColumnName("CategoryName")
                .IsRequired()
                .HasMaxLength(15);

            Property(x => x.Description)
                .HasColumnType("ntext");
        }
    }
}

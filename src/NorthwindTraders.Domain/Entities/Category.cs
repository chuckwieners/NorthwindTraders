using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NorthwindTraders.Domain.Entities
{
    public class Category : Entity<int>
    {
        public Category()
        {
            //NOTE: instantiating an empty collection to the 
            //products collection to prevent it from being NULL
            Products = new HashSet<Product>();
        }

        [Required]
        [StringLength(15)]
        public string Name { get; set; }
        [MaxLength(5000)]
        public string Description { get; set; }

        //Although there are no FK for products within a category
        //we can use the power of EF to automatically map all products
        //in this category
        public virtual ICollection<Product> Products { get; set; }
        //this creates the navigational property that we typically see in the
        //edmx file.
    }
}

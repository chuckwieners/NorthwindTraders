using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NorthwindTraders.Domain.ComplexTypes;

namespace NorthwindTraders.Domain.Entities
{
    public class Supplier : Entity<int>
    {
        public Supplier()
        {
            //NOTE: instantiating an empty collection to the 
            //products collection to prevent it from being NULL
            Products = new HashSet<Product>();
        }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        [Required]
        [MaxLength(30)]
        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }
        [MaxLength(30)]
        [Display(Name = "Contact Title")]
        public string ContactTitle { get; set; }
        public Address Address { get; set; }
        [MaxLength(24)]
        public string Phone { get; set; }
        [MaxLength(24)]
        public string Fax { get; set; }
        [Url] //this will cause issues with the current URLs within the Northwind DB
        [Display(Name = "Home Page")]
        public string HomePage { get; set; }

        //Although there are no FK for products within a category
        //we can use the power of EF to automatically map all products
        //in this category
        public virtual ICollection<Product> Products { get; set; }
    }
}
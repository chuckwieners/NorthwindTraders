using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NorthwindTraders.Domain.Entities
{
    public class Product : Entity<int>
    {
        public Product()
        {
            //OrderDetails = new HashSet<OrderDetail>();
        }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
        [MaxLength(20)]
        [Display(Name = "Quantity Per Unit")]
        public string QuantityPerUnit { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = false, ConvertEmptyStringToNull = true, DataFormatString = "{0:C}")]
        [Display(Name = "Price")]
        public decimal? Price { get; set; }
        [Display(Name = "Units In Stock")]
        public short? UnitsInStock { get; set; }
        [Display(Name = "Units On Order")]
        public short? UnitsOnOrder { get; set; }
        [Display(Name = "Reorder Level")]
        public short? ReorderLevel { get; set; }
        [Required] //<--redundant since this bool is NOT nullable
        public bool IsDiscontinued { get; set; }

        public virtual Category Category { get; set; }
        public virtual Supplier Supplier { get; set; }

        //NOTE: Leave this commented out until this and other 
        //entities and relationships are implemented
        //[Display(Name = "Order Details")]
        //public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
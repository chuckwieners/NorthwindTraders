using System.ComponentModel.DataAnnotations;

namespace NorthwindTraders.Domain.Search.Criteria.Results
{
    /// <summary>
    /// This class is used as a model to slim down to the
    /// minimum information needed for a search result
    /// </summary>
    //NOTE: if there is a desire to link to the Category or Supplier, 
    //those ID's will need to be added as properties here. Don't forget to set those
    //values in the repository
    public class ProductResult
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        [Display(Name = "Quantity Per Unit")]
        public string QuantityPerUnit { get; set; }
        [Display(Name = "Price")]
        public decimal? Price { get; set; }
        [Display(Name = "Units in Stock")]
        public short? UnitsInStock { get; set; }
        [Display(Name = "Category")]
        public string CategoryName { get; set; }
        [Display(Name = "Supplier")]
        public string SupplierName { get; set; }
    }
}

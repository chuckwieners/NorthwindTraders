using NorthwindTraders.Domain.Enums;

namespace NorthwindTraders.Domain.Search.Criteria
{
    /// <summary>
    /// Product search criteria
    /// </summary>
    public class ProductSearchCriteria : ISearchCriteria
    {
        public ProductSearchCriteria()
        {
            //NOTE: we could use the construct to set the default search criteria
            //for newly constructed ProductSearchCriteria or it can be done after
            //the instantiation
            //Discontinued = DiscontinuedEnum.No;
        }

        public string SearchText { get; set; }
        //NOTE: using an enum here will make it more readable in the code
        //and can be reused in the client implementations. Some ORMs will
        //map enums so, this could be used in the Product entity as well.
        public DiscontinuedEnum? Discontinued { get; set; }
        public int? CategoryId { get; set; }
        public int? SupplierId { get; set; }
        //other criteria can be added later
    }
}
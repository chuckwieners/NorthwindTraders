using System.Collections.Generic;
using NorthwindTraders.Domain.Entities;
using NorthwindTraders.Domain.Search.Criteria;
using NorthwindTraders.Domain.Search.Criteria.Results;

namespace NorthwindTraders.Domain.Repositories
{
    /// <summary>
    /// Products repository interface. This allows for future methods to be
    /// defined here beyond
    /// the methods in the base IRepository interface.
    /// </summary>
    public interface IProducts : IRepository<Product, int>, 
        ISearchable<IList<ProductResult>, ProductSearchCriteria>
    {
    }
}
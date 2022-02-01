namespace NorthwindTraders.Domain.Search.Criteria
{
    /// <summary>
    /// Generic interface that will define a Search method that expected a specified Criteria type
    /// and returns a specified Result.
    /// </summary>
    /// <typeparam name="TResults">Type of result to be returned</typeparam>
    /// <typeparam name="TCriteria">Type of criteria to except</typeparam>
    public interface ISearchable<out TResults, in TCriteria> 
        where TCriteria : ISearchCriteria
    {
        TResults Search(TCriteria criteria);
    }
}
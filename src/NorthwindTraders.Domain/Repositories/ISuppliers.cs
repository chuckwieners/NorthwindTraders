using System.Collections.Generic;
using NorthwindTraders.Domain.Entities;

namespace NorthwindTraders.Domain.Repositories
{
    /// <summary>
    /// Suppliers repository interface. This allows for future methods to be defined here beyond
    /// the methods in the base IRepository interface.
    /// </summary>
    public interface ISuppliers : IRepository<Supplier, int>
    {
        //NOTE: typically an ALL property on a repo is a code smell but domain types like categories 
        //where they populate dropdowns. it is somewhat unavoidable.
        //NOTE: since this property only has a getter (missing the setter) this becomes 
        //a readonly property
        List<Supplier> All { get; }
    }
}
namespace NorthwindTraders.Domain.Repositories
{
    /// <summary>
    /// This interface sets the base expectations for all repositories.
    /// Although this can be useful it can and will fail as this domain 
    /// is further fleshed out. The purpose of using it now is to reinforce
    /// the uses of interfaces. What would fail? The update() and add() methods
    /// may end up needing different data types.
    /// </summary>
    /// <typeparam name="TEntity">
    ///     Has a constraint to only allow types that inherit 
    ///     from the Entity class
    /// </typeparam>
    /// <typeparam name="TPrimaryKey">
    ///     Data type of the Entity's Primary Key
    /// </typeparam>
    public interface IRepository<TEntity, TPrimaryKey>
        where TEntity : Entity<TPrimaryKey>
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        TEntity FindBy(TPrimaryKey entityId);
    }
}
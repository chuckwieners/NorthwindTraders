namespace NorthwindTraders.Domain
{
    //NOTE: class is abstract to enforce inheritance
    /// <summary>
    /// This abstract class implements the IEntity interface and
    /// is a generic to allow the Id data type to be set 
    /// upon it's implementation
    /// </summary>
    /// <typeparam name="T">Datatype of the Primary Key</typeparam>
    public abstract class Entity<T> : IEntity
    {
        /// <summary>
        /// The PrimaryKey of the Entity
        /// </summary>
        public T Id { get; set; }
    }
}

using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Reflection;
using NorthwindTraders.Data.EF.Entity.Maps;
using NorthwindTraders.Domain.Entities;

namespace NorthwindTraders.Data.EF
{
    public class NorthwindContext : DbContext
    {
        //NOTE: the connection string being hard coded in the ctor.
        // typically you would have this passed into the ctor during instantiation (dependency injection - DI)
        // or injected via a Inversion of control - IOC container

        public NorthwindContext()
            : base("name=Northwind") { }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }

        /// <summary>
        ///NOTE: this is where conventions can be added.
        /// the code below is loading the conventions individually (all new conventions will need to be added individually)
        /// another option would be to use Reflection to find all conventions within a specified assembly. Using reflection will
        /// alleviate you or other developers from having to remember to add conventions here.
        /// the mappings are loaded via reflection. This creates more or less an architectural convention.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //NOTE: can explicitly add each individual mapping class here
            //      modelBuilder.Configurations.Add(new CategoryMap()); //<---- and do this for each and every map
            //or use reflection to find all mapping classes
            //basically creating a convention within this codebase (API)
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(typeof(CategoryMap)));

            //NOTE: conventions to be loaded (implemented). Implementation is exactly like the maps
            //modelBuilder.Conventions.Add(new MoneyConvention());
        }

        /// <summary>
        /// NOTE: we can hook into this method call and do things. An example is 
        /// if there are entities with Auditing information (modified By or On) we could
        /// use an interface or base class entities to know which entities are auditable and
        /// set those values in one place and not have to do it in each save implementation
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            try
            {
                // Your code...
                // Could also set the Creation and Update Dates on Auditable entities
                // Could also be before try if you know the exception occurs in SaveChanges

                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    var logMessage = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        var logInnerMessage = string.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
    }
}

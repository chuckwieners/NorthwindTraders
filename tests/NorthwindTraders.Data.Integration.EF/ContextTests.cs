using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindTraders.Data.EF;

namespace NorthwindTraders.Data.Integration.EF
{
    /// <summary>
    /// These tests are to verify the object mappings of children.
    /// Most of these tests are just getting the first item except for when a specific object is needed for the test to pass.
    /// integration tests can easily break since they are dependent on external data
    /// </summary>
    [TestClass]
    public class ContextTests
    {
        private NorthwindContext context;

        [TestInitialize]
        public void Set()
        {
            context = new NorthwindContext();
        }

        [TestMethod]
        public void CanGetACategory()
        {
            var category = context.Categories.FirstOrDefault();
            //TODO: could verify for each entity property being mapped
            Assert.IsNotNull(category, "should retrieve category");
            Assert.IsTrue(category.Products.Any(), "category should have products");
        }

        [TestMethod]
        public void CanGetAProduct()
        {
            var product = context.Products.FirstOrDefault();
            //TODO: could verify for each entity property being mapped
            Assert.IsNotNull(product, "should retrieve product");
            Assert.IsNotNull(product.Category, "product should have a category");
            Assert.IsNotNull(product.Supplier, "product should have a supplier");
        }

        [TestMethod]
        public void CanGetEmployee()
        {
            var manager = context.Employees.FirstOrDefault(x => x.Id == 2); //<---getting specific employee to test against
            Assert.IsNotNull(manager, "should retrieve employee");
            Assert.IsTrue(manager.Subordinates.Any(), "employee should have subordinates");
            Assert.IsNull(manager.Supervisor, "this manager has no supervisor");

            var subordinate = context.Employees.FirstOrDefault(x => x.Id == 5);
            Assert.IsNotNull(subordinate.Supervisor, "this subordinate should have a supervisor");
            Assert.IsNotNull(subordinate.Address, "employee should have address");
        }

        [TestMethod]
        public void CanGetASupplier()
        {
            var supplier = context.Suppliers.FirstOrDefault();
            //TODO: could verify for each entity property being mapped
            Assert.IsNotNull(supplier, "should retrieve supplier");
            Assert.IsTrue(supplier.Products.Any(), "supplier should have products");
            Assert.IsNotNull(supplier.Address.Street, "customer should have a street address");
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindTraders.Data.ADO.Repositories;

namespace NorthwindTraders.Data.Integration.ADO
{
    [TestClass]
    public class ProductsTest
    {
        [TestMethod]
        public void CanGetProductById()
        {
            var repo = new Products();
            var results = repo.FindBy(4);

            Assert.IsNotNull(results);
            //TODO: verify all properties are as expected
            Assert.IsNotNull(results.Name);
        }
    }
}

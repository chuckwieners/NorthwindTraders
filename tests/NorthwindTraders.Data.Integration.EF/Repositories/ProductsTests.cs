using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindTraders.Data.EF.Repositories;
using NorthwindTraders.Domain.Entities;
using NorthwindTraders.Domain.Enums;
using NorthwindTraders.Domain.Repositories;
using NorthwindTraders.Domain.Search.Criteria;

namespace NorthwindTraders.Data.Integration.EF.Repositories
{
    [TestClass]
    public class ProductsTests
    {
        //class field
        private Products _repo;

        [TestInitialize]
        public void Setup()
        {
            _repo = new Products();
        }

        [TestMethod]
        public void CanGetProductById()
        {
            var result = _repo.FindBy(14);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Name == "Tofu");
            Assert.IsTrue(result.Category.Name == "Produce");
        }

        [TestMethod]
        public void CanUpdateProduct()
        {
            //setup by getting an existing product from DB
            var product = _repo.FindBy(14);

            //verify this is the expected product
            Assert.IsTrue(product.Name == "Tofu");

            //modify product name
            product.Name = "Tofu Your fo";
            //save changes
            _repo.Update(product);

            //requery for the updated product 'tofu'
            var updatedProduct = _repo.FindBy(14);
            Assert.IsTrue(updatedProduct.Name == "Tofu Your fo");

            //revert these changes in a clean up test method
        }

        [TestMethod]
        public void CanAddNewProduct()
        {
            //create a new product entity
            var product = new Product();
            //assign values to the new product
            product.Name = "test product";
            product.Price = 1.99m;
            product.IsDiscontinued = true;
            product.UnitsInStock = 2;
            product.SupplierId = 1;
            product.CategoryId = 1;
            //save
            _repo.Add(product);

            //verify product was saved
            var criteria = new ProductSearchCriteria()
            {
                SearchText = "test product"
            };
            var newProduct = _repo.Search(criteria);
            //The new product should have been saved to the DB
            Assert.IsTrue(newProduct.Count == 1);
        }

        [TestMethod]
        public void CanSearchProductsByName()
        {
            var criteria = new ProductSearchCriteria()
            {
                SearchText = "chef",
                Discontinued = null,
                CategoryId = null
            };

            var results = _repo.Search(criteria);
            Assert.IsTrue(results.Count == 2);
        }

        [TestMethod]
        public void CanSearchProductsByNameAndDiscontinued()
        {
            var criteria = new ProductSearchCriteria()
            {
                SearchText = "chef",
                Discontinued = DiscontinuedEnum.Yes,
                CategoryId = null
            };

            var results = _repo.Search(criteria);
            Assert.IsTrue(results.Count == 1);
        }



        [TestCleanup]
        public void CleanUp()
        {
            //clean  up code (db changes) for the update tests
            var product = _repo.FindBy(14);
            product.Name = "Tofu";
            _repo.Update(product);

            //clean up code (db changes) for the add tests
            var criteria = new ProductSearchCriteria()
            {
                SearchText = "test product"
            };
            var newProducts = _repo.Search(criteria);
            //remove one or many 'test product' products from db
            foreach (var p in newProducts)
            {
                //since the Search() returns a list of ProductResult
                //and the Remove() needs a Product datatype.
                //we have to use the FindBy() to get the correct datatype
                //before removing it from the db
                var prod = _repo.FindBy(p.ProductId);
                _repo.Remove(prod);
            }

        }
    }
}

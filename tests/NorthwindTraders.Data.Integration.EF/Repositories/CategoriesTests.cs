using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindTraders.Data.EF.Repositories;
using NorthwindTraders.Domain.Entities;

namespace NorthwindTraders.Data.Integration.EF.Repositories
{
    [TestClass]
    public class CategoriesTests
    {
        //class field
        private Categories _repo;

        [TestInitialize] //<-test setup attribute
        public void Setup() //<-method name could be anything
        {
            _repo = new Categories();
        }

        //test that verifies that GetAll() returns all categories
        [TestMethod]
        public void CanGetAllCategories()
        {
            //ACT: call repo method under test
            var list = _repo.All;
            //ASSERT: verify meaningful conditions
            Assert.IsNotNull(list, "should not be null");
            Assert.IsTrue(list.Count() == 8);
            //CleanUp
        }

        [TestMethod]
        public void CanGetCategoryById()
        {
            var result = _repo.FindBy(2);

            Assert.IsNotNull(result, "should not be null");

            Assert.IsTrue(result.Name == "Condiments"
                , "name should be condiments");

            Assert.IsTrue(result.Products.Count() == 12);
        }

        [TestMethod]
        public void CanUpdateCategory()
        {
            //setup by getting a category
            var category = _repo.FindBy(1);

            //verify category name is 'Beverages'
            Assert.IsTrue(category.Name == "Beverages", "cat name should be Beverages");
            //modify cat name
            category.Name = "Drinks";
            //save changes
            _repo.Update(category);

            //requery for the updated category 'beverages'
            var updatedCategory = _repo.FindBy(1);
            //Now verify that the update was persisted in the DB
            Assert.IsTrue(updatedCategory.Name == "Drinks", "cat name should be Drinks");


        }

        [TestMethod]
        public void CanAddNewCategory()
        {
            //create new category
            var category = new Category();
            //assign values to category
            category.Name = "Cat";
            //call repo.Add()
            _repo.Add(category);

            //need to verify the category was saved in the DB
            var newCategory = _repo.All.FirstOrDefault(x => x.Name == "Cat");
            Assert.IsNotNull(newCategory, "new category should not be null");
            //remove category in clean up or rely on the 'remove' test???
        }

        [TestCleanup] //<--test clean up attribute
        public void CleanUp() //<-- method name could be anything
        {
            //clean up code (db changes) for the Update tests
            var category = _repo.FindBy(1);
            category.Name = "Beverages";
            _repo.Update(category);

            //clean up code (db changes) for the Add tests
            var newCategories = _repo.All.Where(x => x.Name == "Cat");
            //since there is a potential of having 'many' new catgories
            //loop and remove all matches
            foreach (var cat in newCategories.ToList())
            {
                _repo.Remove(cat);
            }
        }
    }
}

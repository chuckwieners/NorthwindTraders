using System;
using System.Collections.Generic;
using System.Linq;
using NorthwindTraders.Domain.Entities;
using NorthwindTraders.Domain.Repositories;

namespace NorthwindTraders.Data.EF.Repositories
{
    public class Categories : ICategories
    {
        private readonly NorthwindContext _dbContext;

        public Categories()
        {
            //NOTE: where is the dbContext instantiating? 
            //          DbContext's are big and bulky so you want to try to reuse one as much as possible.
            //          Another reason why you want to reuse a single dbContext. There are issues that arise when the results from one dbContexts are passed to a second dbContext on a Save or Update that will cause issues or at least fail.
            //a) new up a dbContext in each repo, or have a base class where it is instantiated
            //b) create a singleton that is used by all repositories
            //c) use an inversion of control container to instantiate and manage the singleton
            _dbContext = new NorthwindContext();
        }

        public List<Category> All
        {
            get { return _dbContext.Categories.ToList(); }
        }

        public Category FindBy(int id)
        {
            return _dbContext.Categories.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Category category)
        {
            var entity = _dbContext.Categories.Single(x => x.Id == category.Id);

            entity.Name = category.Name.Trim();
            entity.Description = category.Description.Trim();

            _dbContext.SaveChanges();
        }

        public void Add(Category category)
        {
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
        }

        public void Remove(Category category)
        {
            throw new NotImplementedException();
        }
    }
}

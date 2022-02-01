using System;
using System.Collections.Generic;
using System.Linq;
using NorthwindTraders.Domain.Entities;
using NorthwindTraders.Domain.Enums;
using NorthwindTraders.Domain.Repositories;
using NorthwindTraders.Domain.Search.Criteria;
using NorthwindTraders.Domain.Search.Criteria.Results;

namespace NorthwindTraders.Data.EF.Repositories
{
    public class Products : IProducts
    {
        private readonly NorthwindContext _dbContext;
        
        public Products()
        {
            //NOTE: where is the dbContext instantiating? 
            //          DbContext's are big and bulky so you want to try to reuse one as much as possible.
            //          Another reason why you want to reuse a single dbContext. There are issues that arise when the results from one dbContexts are passed to a second dbContext on a Save or Update that will cause issues or at least fail.
            //a) new up a dbContext in each repo, or have a base class where it is instantiated
            //b) create a singleton that is used by all repositories
            //c) use an inversion of control container to instantiate and manage the singleton
            _dbContext = new NorthwindContext();
        }

        public IList<ProductResult> Search(ProductSearchCriteria criteria)
        {
            //NOTE: a predicate builder would be better here... but using differed execution is the next best thing

            IQueryable<Product> results;

            //NOTE: the results needs to be initialized with some results
            if (criteria.Discontinued != null)
            {
                var isDiscontinued = criteria.Discontinued.ToString() == DiscontinuedEnum.Yes.ToString();
                results = _dbContext.Products.Where(x => x.IsDiscontinued == isDiscontinued);
            }
            else
                //default to ALL products (discontinued or not) if the criteria IsDicontinued is null
                results = _dbContext.Products;

            if (criteria.CategoryId != null)
                results = results.Where(x => x.CategoryId == criteria.CategoryId);

            if (criteria.SupplierId != null)
                results = results.Where(x => x.SupplierId == criteria.SupplierId);

            //if the SearchText is NOT null and using the results left .. continue to filter
            if (!string.IsNullOrEmpty(criteria.SearchText))
            {
                //NOTE: do we find matches that Contain or StartsWith??
                results = results.Where(x =>
                    x.Name.Contains(criteria.SearchText) ||
                    x.Category.Name.Contains(criteria.SearchText) ||
                    x.Supplier.Name.Contains(criteria.SearchText));
            }

            //NOTE: this is an example of flattening an object using projection.
            // this is appropriate when only a few values are needed and/or to speed up queries
            return results.Select(x => new ProductResult
            {
                ProductId = x.Id,
                Name = x.Name,
                Price = x.Price,
                UnitsInStock = x.UnitsInStock,
                CategoryName = x.Category.Name,
                SupplierName = x.Supplier.Name,
            }).ToList();
        }


        public Product FindBy(int id)
        {
            return _dbContext.Products.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Product product)
        {
            var entity = _dbContext.Products.Single(x => x.Id == product.Id);

            entity.Name = product.Name;
            entity.SupplierId = product.SupplierId;
            entity.CategoryId = product.CategoryId;
            entity.Price = product.Price;
            entity.UnitsInStock = product.UnitsInStock;
            entity.UnitsOnOrder = product.UnitsOnOrder;
            entity.ReorderLevel = product.ReorderLevel;
            entity.IsDiscontinued = product.IsDiscontinued;

            _dbContext.SaveChanges();
        }

        public void Add(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
        }

        public void Remove(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
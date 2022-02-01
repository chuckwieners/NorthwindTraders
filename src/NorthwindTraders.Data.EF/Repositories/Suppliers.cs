using System;
using System.Collections.Generic;
using System.Linq;
using NorthwindTraders.Domain.Entities;
using NorthwindTraders.Domain.Repositories;

namespace NorthwindTraders.Data.EF.Repositories
{
    public class Suppliers : ISuppliers
    {
        private readonly NorthwindContext _dbContext;

        public Suppliers()
        {
            _dbContext = new NorthwindContext();
        }

        public List<Supplier> All
        {
            get { return _dbContext.Suppliers.ToList(); }
        }

        public Supplier FindBy(int id)
        {
            return _dbContext.Suppliers.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Supplier supplier)
        {
            var entity = _dbContext.Suppliers.Single(x => x.Id == supplier.Id);

            entity.Name = supplier.Name;
            entity.ContactName = supplier.ContactName;
            entity.ContactTitle = supplier.ContactTitle;
            entity.Fax = supplier.Fax;
            entity.HomePage = supplier.HomePage;
            entity.Phone = supplier.Phone;
            //the below code could be handled much differently within a 
            //domain driven design architecture.
            entity.Address = supplier.Address;

            _dbContext.SaveChanges();
        }

        public void Add(Supplier supplier)
        {
            _dbContext.Suppliers.Add(supplier);
            _dbContext.SaveChanges();
        }

        public void Remove(Supplier id)
        {
            throw new NotImplementedException();
        }
    }
}
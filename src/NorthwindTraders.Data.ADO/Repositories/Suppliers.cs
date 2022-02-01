using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthwindTraders.Domain.Entities;
using NorthwindTraders.Domain.Repositories;

namespace NorthwindTraders.Data.ADO.Repositories
{
    public class Suppliers : ISuppliers
    {
        public List<Supplier> All
        {
            get { throw new NotImplementedException(); }
        }

        public void Add(Supplier entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Supplier entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Supplier entity)
        {
            throw new NotImplementedException();
        }

        public Supplier FindBy(int entityId)
        {
            throw new NotImplementedException();
        }
    }
}

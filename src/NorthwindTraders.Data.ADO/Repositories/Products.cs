using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NorthwindTraders.Domain.Entities;
using NorthwindTraders.Domain.Repositories;
using NorthwindTraders.Domain.Search.Criteria;
using NorthwindTraders.Domain.Search.Criteria.Results;

/*
 * implementing this should help show the pain of mimicking what an ORM (EntityFramework) provides.
 * it can also show why there are arguements of not pushing the Domain Entities down to the client layers, because
 * of the potential dependencies of providing the entity children and grandchildren data. 
 * Example: Product.Orders.Employee (such code would require an massive SQL select statement along with all the code to map that data into the object tree)
 */

namespace NorthwindTraders.Data.ADO.Repositories
{
    public class Products : IProducts
    {
        private readonly SqlConnection _sqlConnection;

        public Products()
        {
            var connStr = System.Configuration.ConfigurationManager.ConnectionStrings["Northwind"].ConnectionString;
            _sqlConnection = new SqlConnection(connStr);
        }

        public Product FindBy(int id)
        {
            using (_sqlConnection)
            {
                var cmd = new SqlCommand
                {
                    CommandText = @"SELECT  
                                      [ProductID]
                                      ,[ProductName]
                                      ,[SupplierID]
                                      ,[CategoryID]
                                      ,[QuantityPerUnit]
                                      ,[UnitPrice]
                                      ,[UnitsInStock]
                                      ,[UnitsOnOrder]
                                      ,[ReorderLevel]
                                      ,[Discontinued]
                                    FROM Products 
                                    WHERE productId = @productId",
                    Connection = _sqlConnection
                };

                cmd.Parameters.AddWithValue("@productId", id);
                _sqlConnection.Open();
                var rdr = cmd.ExecuteReader();
                var product = new Product();
                while (rdr.Read())
                {
                    //only one record should be return ... do we need a while loop then?
                    product.Id = (int) rdr["ProductId"];
                    product.CategoryId = (int) rdr["CategoryId"];
                    //product.Category = //TODO: will need to add a join and instantiate and populate a category child object
                    product.IsDiscontinued = (bool) rdr["Discontinued"];
                    product.Name = rdr["ProductName"].ToString();
                    //product.OrderDetails = //TODO: will need to add a join and instantiate and populate an order details child object
                    product.QuantityPerUnit = rdr["QuantityPerUnit"].ToString();
                    product.ReorderLevel = (short) rdr["ReorderLevel"];
                    product.SupplierId = (int) rdr["SupplierId"];
                    //product.Supplier = //TODO: will need to add a join and instantiate and populate a supplier child object
                    product.Price = (decimal) rdr["UnitPrice"];
                    product.UnitsInStock = (short?) rdr["UnitsInStock"];
                    product.UnitsOnOrder = (short?) rdr["UnitsOnOrder"];
                }
                return product;
            }
        }

        public IList<ProductResult> Search(ProductSearchCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {
            //NOTE: this method and its parameter forces that all property values must be 
            //updated, regardless if the user has changed the values
            throw new NotImplementedException();
        }

        public void Add(Product product)
        {
            throw new NotImplementedException();
        }

        public void Remove(Product product)
        {
            throw new NotImplementedException();
        }
    }
}

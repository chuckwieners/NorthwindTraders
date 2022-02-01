using System.Collections.Generic;
using System.Data.SqlClient;
using NorthwindTraders.Domain.Entities;
using NorthwindTraders.Domain.Repositories;

namespace NorthwindTraders.Data.ADO.Repositories
{
    public class Categories : ICategories
    {
        private readonly SqlConnection _sqlConnection;

        public Categories()
        {
            var connStr = 
                System.Configuration.ConfigurationManager.ConnectionStrings["Northwind"].ConnectionString;
            _sqlConnection = new SqlConnection(connStr);
        }

        public void Add(Category entity)
        {
            //
            using (_sqlConnection)
            {
                //
                var cmd = new SqlCommand(
                    @"INSERT INTO Categories 
                            (CategoryName, Description)
                        VALUES
                            (@name, @description)", _sqlConnection);

                //
                cmd.Parameters.AddWithValue("@name", entity.Name);
                cmd.Parameters.AddWithValue("@description", entity.Description);
                //
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Category entity)
        {
            //
            using (_sqlConnection)
            {
                //
                var cmd = new SqlCommand(
                    @"UPDATE    Categories 
                                SET
                                    CategoryName = @name,
                                    Description = @description
                                WHERE CategoryId = @categoryId", _sqlConnection);

                //
                cmd.Parameters.AddWithValue("@name", entity.Name);
                cmd.Parameters.AddWithValue("@description", entity.Description);
                cmd.Parameters.AddWithValue("@categoryId", entity.Id);
                //
                cmd.ExecuteNonQuery();
            }
        }

        public void Remove(Category entity)
        {
            //
            using (_sqlConnection)
            {
                //
                var cmd = new SqlCommand(@"DELETE FROM Categories WHERE CategoryId = @categoryId", _sqlConnection);
                //
                cmd.Parameters.AddWithValue("@categoryId", entity.Id);
                //
                cmd.ExecuteNonQuery();
            }
        }

        public Category FindBy(int entityId)
        {
            using (_sqlConnection)
            {
                //
                var cmd = new SqlCommand(
                        @"SELECT CategoryId, CategoryName, Description FROM Categories WHERE CategoryId = @categoryId", _sqlConnection);
                //
                cmd.Parameters.AddWithValue("@categoryId", entityId);
                //
                var rdr = cmd.ExecuteReader();
                //
                var category = new Category();
                //
                while (rdr.Read())
                {
                    category.Id = (int) rdr["CategoryId"];
                    category.Name = rdr["CategoryName"].ToString();
                    category.Description = rdr["Description"].ToString();
                }
                //
                return category;
            }
        }

        public List<Category> All
        {
            get
            {
                //
                using (_sqlConnection)
                {
                    //
                    var cmd = new SqlCommand(
                        @"SELECT CategoryId, CategoryName, Description FROM Categories", _sqlConnection);

                    //
                    var rdr = cmd.ExecuteReader();
                    //
                    var list = new List<Category>();
                    //iterate through the reader and build the list of categories to be returned.
                    while (rdr.Read())
                    {
                        var category = new Category
                                       {
                                           Id = (int)rdr["CategoryId"],
                                           Name = rdr["CategoryName"].ToString(),
                                           Description = rdr["Description"].ToString()
                                       };
                        list.Add(category);
                    }
                    //
                    return list;
                }
            }
        }
    }
}

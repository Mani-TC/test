using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Data.Linq;
using System.Web;
using refactor_this.Models;
using Dapper;
using refactor_me.Models;
using refactor_me.Repository;

namespace refactor_this.Repository.Adapters
{
    //TODO: use dispose
    public class Repository<T> : IRepository<T>, IProductRepository, IOptionsRepository
        where T:class 
    {
        private readonly SqlConnection _connection;
        private DataContext _db;

        public Repository()
        {
            _connection = Helpers.NewConnection();
            _db = new DataContext(_connection);
        }

        public IQueryable<Product> All()
        {
            var productsTable = _connection.Query<Product>("SELECT * FROM Product");

            return productsTable.AsQueryable();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Product Find(Guid id)
        {
            return this.All().Single(p => p.Id == id);
        }

        public IQueryable<Product> Find(string name)
        {

            return _connection.Query<Product>("SELECT * FROM Product WHERE name like @p", new { p = "%"+name+"%" })
                .AsQueryable();
        }

        public void Insert(Product newProduct)
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {
            throw new NotImplementedException();
        }

        IQueryable<ProductOption> IOptionsRepository.All()
        {
            return _connection.Query<ProductOption>("SELECT * FROM ProductOption").AsQueryable();
        }

        IEnumerable<T> IRepository<T>.All()
        {
            var query = $"SELECT * FROM {typeof(T).Name} ";

            using (var connection = new SqlConnection(""))
            {
                connection.Open();
                return connection.Query<T>(query);
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
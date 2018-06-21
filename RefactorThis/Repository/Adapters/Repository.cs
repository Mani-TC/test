using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Data.Linq;
using System.Threading.Tasks;
using System.Web;
using refactor_this.Models;
using Dapper;
using refactor_me.Models;
using refactor_me.Repository;
using System.Reflection;

namespace refactor_this.Repository.Adapters
{
    //TODO: use dispose
    public abstract class Repository<T> : IRepository<T>
        where T:Entity<Guid>
    {
        protected Repository()
        {
        }

        public T Find(Guid id)
        {
            var query = $"SELECT * FROM {typeof(T).Name} WHERE Id=@id";

            using (var connection = new SqlConnection(Helpers.DbConnectionString))
            {
                connection.Open();
                return connection.QuerySingle<T>(query, new{id});
            }
        }

        public IEnumerable<T> Find(string name)
        {
            var query = $"SELECT * FROM {typeof(T).Name} WHERE name like @p";

            using (var connection = new SqlConnection(Helpers.DbConnectionString))
            {
                connection.Open();
                return connection.Query<T>(query, new { p = "%" + name + "%" });
            }
        }

        public virtual Task Insert(T entity)
        {
            entity.Id = Guid.NewGuid();
            var columns = GetColumns();
            var stringOfColumns = string.Join(", ", columns);
            var stringOfParameters = string.Join(", ", columns.Select(e => "@" + e));
            var query = $"insert into {typeof(T).Name} ({stringOfColumns}) values ({stringOfParameters})";

            using (var connection = new SqlConnection(Helpers.DbConnectionString))
            {
                connection.Open();
                connection.Execute(query, entity);
            }
            return Task.FromResult(0);
        }

        public Task Update(Guid id, T entity)
        {
            var columns = GetColumns();
            var stringOfColumns = string.Join(", ", columns.Select(e => $"{e} = @{e}"));
            var query = $"update {typeof(T).Name} set {stringOfColumns} where Id = @Id";

            using (var connection = new SqlConnection(Helpers.DbConnectionString))
            {
                connection.Open();
                connection.Execute(query, entity);
            }
            return Task.FromResult(0);
        }

        public Task Delete(Guid id)
        {
            var query = $"delete from {typeof(T).Name} where Id = @Id";

            using (var connection = new SqlConnection(Helpers.DbConnectionString))
            {
                connection.Open();
                connection.Execute(query, new{id});
            }
            return Task.FromResult(0);
        }


        IEnumerable<T> IRepository<T>.All()
        {
            var query = $"SELECT * FROM {typeof(T).Name} ";

            using (var connection = new SqlConnection(Helpers.DbConnectionString))
            {
                connection.Open();
                return connection.Query<T>(query);
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        private IEnumerable<string> GetColumns()
        {
            return typeof(T)
                .GetProperties()
                .Where(e =>!e.PropertyType.GetTypeInfo().IsGenericType)
                .Select(e => e.Name);
        }
    }
}
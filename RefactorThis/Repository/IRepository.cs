using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace refactor_me.Repository
{
    public interface IRepository<T> : IDisposable where T: class
    {
        IEnumerable<T> All();
        T Find(Guid id);
        IEnumerable<T> Find(string name);

        Task Insert(T entity);

        Task Update(Guid id, T entity);

        Task Delete(Guid id);
    }
}
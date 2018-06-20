using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace refactor_me.Repository
{
    public interface IRepository<T> : IDisposable where T: class
    {
        IEnumerable<T> All();

    }
}
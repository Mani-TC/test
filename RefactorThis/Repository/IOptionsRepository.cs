using refactor_me.Models;
using System.Linq;
using refactor_me.Repository;

namespace refactor_this.Repository
{
    public interface IOptionsRepository:IRepository<ProductOption>
    {
    }
}
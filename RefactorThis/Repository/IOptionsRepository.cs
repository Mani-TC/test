using refactor_me.Models;
using System.Linq;

namespace refactor_this.Repository
{
    public interface IOptionsRepository
    {
        IQueryable<ProductOption> All();
    }
}
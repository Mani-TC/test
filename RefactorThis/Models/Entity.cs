using System.Data.Linq;

namespace refactor_this.Models
{
    public abstract class Entity<T>
    {
        public virtual T Id { get; set; }
        public abstract void Change(Entity<T> target) ;
    }
}
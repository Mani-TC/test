using refactor_this.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using refactor_me.Repository;

namespace refactor_this.Repository
{
    public interface IProductRepository: IRepository<Product>
    {
        //IQueryable<Product> All();

        //Product Find(Guid id);

        //IQueryable<Product> Find(string name);

        //void Insert(Product newProduct);

        //void Update(Product product);

        //void Delete(Guid id);
    }

}
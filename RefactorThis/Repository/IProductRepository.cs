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
        //specific implementations shall be done here
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using refactor_this.Models;
using refactor_this.Repository;
using refactor_this.Repository.Adapters;

namespace refactor_me.Repository.Adapters
{
    public class ProductRepository:Repository<Product>, IProductRepository
    {
    }
}
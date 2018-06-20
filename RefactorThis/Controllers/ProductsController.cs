using System;
using System.Net;
using System.Web.Http;
using refactor_this.Models;
using refactor_this.Repository;
using System.Collections.Generic;
using System.Linq;
using refactor_me.Repository;
using refactor_me.Repository.Adapters;

namespace refactor_this.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private readonly IProductRepository _repository;

        public ProductsController()
        {
            _repository = new ProductRepository();
        }
        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult Get(Guid id)
        {
            var product = _repository.Find(id);

            if (product != null)
                return Ok(product);

            return NotFound();

        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(_repository.All());
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult Get(string name)
        {
            var list = _repository.Find(name);

            if(list.Any())
                return Ok(list);

            return NotFound();
        }



        [Route]
        [HttpPost]
        public IHttpActionResult Create(Product product)
        {
            _repository.Insert(product);

            if (product != null)
            {
                return Created<Product>(Request.RequestUri + "/"+ product.Id.ToString(), product);
            }

            return Conflict();
        }

        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Update(Guid id, Product product)
        {
            var entity = _repository.Find(id);

            if (entity == null)
            {
                return Conflict();
            }

            entity.Change(product);
            _repository.Update(id, entity);
            return Ok(new {Message="Updated", Data = entity});
 }

        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(Guid id)
        {
            var entity = _repository.Find(id);

            if (entity == null)
                return Conflict();

            _repository.Delete(id);
            return Ok(new {Message = "Deleted", Data = id});
        }

        
    }
}

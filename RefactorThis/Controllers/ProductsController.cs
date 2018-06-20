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
            //return Ok(_repository.All().ToList());
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
        public void Create(Product product)
        {
            product.Save();
        }

        [Route("{id}")]
        [HttpPut]
        public void Update(Guid id, Product product)
        {
            var orig = new Product(id)
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                DeliveryPrice = product.DeliveryPrice
            };

            if (!orig.IsNew)
                orig.Save();
        }

        [Route("{id}")]
        [HttpDelete]
        public void Delete(Guid id)
        {
            var product = new Product(id);
            product.Delete();
        }

        
    }
}

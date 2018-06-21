using refactor_this.Repository.Adapters;
using refactor_this.Repository;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using refactor_me.Models;
using refactor_me.Repository.Adapters;

namespace refactor_me.Controllers
{
    [RoutePrefix("api/products/{productId}")]
    public class OptionsController : ApiController
    {
        private readonly IOptionsRepository _repository;
        private readonly ProductRepository _productRepository;

        public OptionsController()
        {
            _repository = new OptionRepository();
            _productRepository = new ProductRepository();
        }

        public OptionsController(IOptionsRepository repository)
        {
            _repository = repository;
        }

        [Route("options")]
        [HttpGet]
        public IHttpActionResult Get(Guid productId)
        {
            var options = _repository.All().Where(p => p.ProductId == productId);

            if(options.Any())
                return Ok(options.AsEnumerable());

            return NotFound();
            //return new ProductOptions(productId);
        }
        
        [Route("options/{id}")]
        [HttpGet]
        public IHttpActionResult GetOption(Guid productId, Guid id)
        {
            var option = _repository.Find(id);
            if (option !=null)
                return Ok(option);

            return NotFound();
        }

        [Route("options")]
        [HttpPost]
        public IHttpActionResult Create(Guid productId, ProductOption option)
        {

            var product = _productRepository.Find(productId);

            if (product == null)
                return NotFound();

            option.ProductId = productId;
            _repository.Insert(option);

            return Ok(new {Message = "Created successfully", Data = option});
        }

        [Route("options/{id}")]
        [HttpPut]
        public IHttpActionResult Update(Guid id, ProductOption option)
        {
            var entity = _repository.Find(id);

            if (entity == null)
            {
                return Conflict();
            }

            entity.Change(option);
            _repository.Update(id, entity);
            return Ok(new { Message = "Updated", Data = entity });
        }

        [Route("options/{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(Guid id)
        {
            var entity = _repository.Find(id);

            if (entity == null)
                return Conflict();

            _repository.Delete(id);
            return Ok(new { Message = "Deleted", Data = id });
        }

        /*
        [Route("{productId}/options")]
        [HttpPost]
        public void CreateOption(Guid productId, ProductOption option)
        {
            option.ProductId = productId;
            option.Save();
        }

        [Route("{productId}/options/{id}")]
        [HttpPut]
        public void UpdateOption(Guid id, ProductOption option)
        {
            var orig = new ProductOption(id)
            {
                Name = option.Name,
                Description = option.Description
            };

            if (!orig.IsNew)
                orig.Save();
        }

        [Route("{productId}/options/{id}")]
        [HttpDelete]
        public void DeleteOption(Guid id)
        {
            var opt = new ProductOption(id);
            opt.Delete();
        }
        */
    }
}

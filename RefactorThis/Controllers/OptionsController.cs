﻿using refactor_this.Repository.Adapters;
using refactor_this.Repository;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace refactor_me.Controllers
{
    [RoutePrefix("api/products/{productId}")]
    public class OptionsController : ApiController
    {
        private readonly IOptionsRepository _repository;

        public OptionsController()
        {
            _repository = new Repository();
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
        /*
        [Route("{productId}/options/{id}")]
        [HttpGet]
        public ProductOption GetOption(Guid productId, Guid id)
        {
            var option = new ProductOption(id);
            if (option.IsNew)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return option;
        }

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScefHR.Models;
using ScefHR.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ScefHR.Controllers
{
    [Route("api/[controller]")]
    public class EntitiesController : Controller
    {
        private IEntityService _entityService;
        private IMapper _mapper;

        public EntitiesController(IEntityService entityService, IMapper mapper)
        {
            _entityService = entityService;
            _mapper = mapper;
        }


        // GET: api/<controller>
        [HttpGet("[action]")]
        [Authorize(Policy = "ApiAdmin")]
        public IActionResult AdminGet()
        {
            var userId = User.Claims.First(c => c.Type == "id").Value;
            var res = _entityService.Read(userId);
            return Ok(res);
        }
        [HttpGet("[action]")]
        [Authorize(Policy = "ApiSuper")]
        public IActionResult SGet()
        {
            var res = _entityService.SRead();
            return Ok(res);
        }

        [HttpGet]
        [Authorize(Policy = "ApiAdmin")]
        public IActionResult Get()
        {
            var userId = User.Claims.First(c => c.Type == "id").Value;
            var res = _entityService.Read(userId);
            return Ok(res);
        }


        // POST api/<controller>
        [HttpPost]
        [Authorize(Policy = "ApiSuper")]
        public IActionResult Post([FromBody]string value)
        {
            _entityService.Create(value);
            return Ok();
        }

        // PUT api/<cont
        [HttpPut("{id}")]
        [Authorize(Policy = "ApiSuper")]
        public IActionResult Put(int id, [FromBody]string value)
        {
            _entityService.Update(null);
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "ApiSuper")]
        public IActionResult Delete(int id)
        {
            _entityService.Delete(0);
            return Ok();
        }
    }
}

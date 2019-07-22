using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_entityService.Read(null));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
           
            return Ok(_entityService.Read(null));
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]string value)
        {
            AppUser appUser = new AppUser{ FirstName= "Ahmed" , LastName= "Saadawi"};
            Employee employee = new Employee { Identity = appUser };
            Entity entity = new Entity { Name = "المؤسسة", NumberOfEmployees= 1 };

            entity.Employees.Add(employee);
            return Ok(_entityService.Create(entity));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]string value)
        {
            _entityService.Update(null);
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _entityService.Delete(1);
            return Ok();
        }
    }
}

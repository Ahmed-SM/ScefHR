using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScefHR.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ScefHR.Controllers
{
    [Route("api/[controller]")]
    public class EmployeesController : Controller
   {
        private IEmployeeService _employeeService;
        private IMapper _mapper;

        public EmployeesController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }


        // GET: api/<controller>
        [HttpGet]
        //[Authorize(AuthenticationSchemes = "JwtBearer")]
        public IActionResult Get()
        {
            return Ok(_employeeService.Read(null));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_employeeService.Read(id));
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]string value)
        {
            //_employeeService.Create();
            return Ok();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]string value)
        {
            _employeeService.Update();
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _employeeService.Delete(id);
            return Ok();
        }
    }
}

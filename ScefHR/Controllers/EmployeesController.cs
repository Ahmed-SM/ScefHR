using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ScefHR.Models;
using ScefHR.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ScefHR.Controllers
{
    [Route("api/[controller]")]
    public class EmployeesController : Controller
   {
        private IEmployeeService _employeeService;
        private IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public EmployeesController(IEmployeeService employeeService, IMapper mapper, UserManager<AppUser> userManager)
      {
            _employeeService = employeeService;
            _mapper = mapper;
            _userManager = userManager;
        }


        // GET: api/<controller>
        [Authorize(Policy = "ApiAdmin")]
        public IActionResult Get()
        {
            var userId = User.Claims.First(c => c.Type == "id").Value;
            var res = _employeeService.Read(userId);
            return Ok(res);
        }
        [HttpGet("[action]")]
        [Authorize(Policy = "ApiSuper")]
        public IActionResult SGet()
        {
            var res = _employeeService.Read();
            return Ok(res);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Authorize(Policy = "ApiAdmin")]
        public IActionResult Get(int id)
        {
            var res = _employeeService.Read(id);
            return Ok(res);
        }
        [HttpGet("[action]/{id}")]
        [Authorize(Policy = "ApiSuper")]
        public IActionResult SGet(int id)
        {
            var res =_employeeService.Read(id);
            return Ok(res);
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
        [Authorize(Policy = "ApiAdmin")]
        public async Task<IActionResult> Put(int id, [FromBody]UpdateEmployee value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _employeeService.Update(id, value);
            return Ok();
        }
        [HttpPut("[action]/{id}")]
        [Authorize(Policy = "ApiSuper")]
        public async Task<IActionResult> SPut(int id, [FromBody]UpdateEmployee value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _employeeService.Update(id, value);
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "ApiAdmin")]
        public IActionResult Delete(int id)
        {
            _employeeService.Delete(id);
            return Ok();
        }
    }
}

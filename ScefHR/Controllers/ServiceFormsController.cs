using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScefHR.Helpers;
using ScefHR.Models;
using ScefHR.Services;

namespace ScefHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceFormsController : ControllerBase
    {
   
        private IServiceFormService _serviceFormService;
        private IMapper _mapper;

        public ServiceFormsController(IServiceFormService serviceFormService, IMapper mapper)
        {
            _serviceFormService = serviceFormService;
            _mapper = mapper;
        }
        [HttpGet("[action]")]
        [Authorize(Policy = "ApiUser")]
        //[Authorize(Policy = "ApiAdmin")]
        public IActionResult GetServiceForms()
        {
            var userId = User.Claims.First(c => c.Type == "id").Value;
            return Ok(_serviceFormService.Read(userId));
        }
        [HttpGet("[action]")]
        [Authorize(Policy = "ApiAdmin")]
        public IActionResult OnHoldServiceForms()
        {
            var userId = User.Claims.First(c => c.Type == "id").Value;
            return Ok(_serviceFormService.AdminOnHoldRead(userId));
        }
        [HttpGet("[action]")]
        [Authorize(Policy = "ApiUser")]
        public IActionResult OnHoldServices()
        {
            var userId = User.Claims.First(c => c.Type == "id").Value;
            return Ok(_serviceFormService.OnHoldRead(userId));
        }
        [HttpGet("[action]")]
        [Authorize(Policy = "ApiAdmin")]
        public IActionResult AcceptedServiceForms()
        {
            var userId = User.Claims.First(c => c.Type == "id").Value;
            return Ok(_serviceFormService.AdminAcceptedRead(userId));
        }
        [HttpGet("[action]")]
        [Authorize(Policy = "ApiUser")]
        public IActionResult AcceptedServices()
        {
            var userId = User.Claims.First(c => c.Type == "id").Value;
            return Ok(_serviceFormService.AcceptedRead(userId));
        }
        [HttpGet("[action]/{id}")]
        [Authorize(Policy = "ApiAdmin")]
        public IActionResult OnHoldServiceForms(int id)
        {
            var userId = User.Claims.First(c => c.Type == "id").Value;
            return Ok(_serviceFormService.AdminOnHoldRead(id, userId));
        }
        [HttpGet("[action]/{id}")]//
        [Authorize(Policy = "ApiAdmin")]
        public IActionResult AcceptedServiceForms(int id)
        {
            var userId = User.Claims.First(c => c.Type == "id").Value;
            return Ok(_serviceFormService.AdmiAcceptRead(id, userId));
        }
        [HttpGet("{id}")]
        [Authorize(Policy = "ApiAdmin")]
        public IActionResult GetServiceForms(int id)
        {
            var userId = User.Claims.First(c => c.Type == "id").Value;
            return Ok(_serviceFormService.Read(userId, id));
        }
        [HttpPost("[action]")]
        [Authorize(Policy = "ApiUser")]
        public IActionResult PostServiceForm([FromBody] ServiceForm serviceForm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = User.Claims.First(c => c.Type == "id").Value;
            _serviceFormService.Create(serviceForm, userId);
            return Ok("Added");
        }

        [HttpPost("[action]/{id}")]
        [Authorize(Policy = "ApiAdmin")]
        public IActionResult OnHoldServicePost(int id)
        {
            var userId = User.Claims.First(c => c.Type == "id").Value;
            _serviceFormService.AdminOnHoldAccept(userId, id);
            return Ok("Accepted");
        }

        [HttpPost("[action]/{id}")]
        [Authorize(Policy = "ApiAdmin")]
        public IActionResult OnHoldRefusePost(int id)
        {
            var userId = User.Claims.First(c => c.Type == "id").Value;
            _serviceFormService.AdminOnHoldRefuse(userId, id);
            return Ok("Refused");
        }
    }
}
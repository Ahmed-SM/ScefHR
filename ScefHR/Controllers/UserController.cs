using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScefHR.Helpers;
using ScefHR.Models;

namespace ScefHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly DataContext _context;
        public UserController(UserManager<AppUser> userManager, DataContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        [Authorize(Policy = "ApiUser")]
        public async Task<Object> GetUserDeatils()
        {
            string userId = User.Claims.First(c => c.Type == "id").Value;

            var user = await _userManager.FindByIdAsync(userId);
            var emp = _context.Employees.Where(i => i.IdentityId == userId).Select(s => new
            {
                s.Identity.FirstName,
                s.Identity.LastName,
                s.Identity.Email,
                s.Entity.Name,
                s.Position,
                s.Gender,
                s.HireDate,
                s.Address,
                s.Birthdate,
                s.Nationality,
                s.PhoneNumber,
                s.Salary
            }).FirstOrDefault();

            return Ok(emp);
        }
        [HttpPut]
        [Authorize(Policy = "ApiUser")]
        public IActionResult PutUserDeatils(IEmployee employee)
        {
            string userId = User.Claims.First(c => c.Type == "id").Value;
            Employee emp =  _context.Employees.Where(i => i.IdentityId == userId).Include(i=>i.Identity).FirstOrDefault();
            if (employee.Address != null)
            {
                emp.Address = employee.Address;
            }
            if (employee.Firstname != null)
            {
                emp.Identity.FirstName = employee.Firstname;
            }
            if (employee.Lastname != null)
            {
                emp.Identity.LastName = employee.Lastname;
            }
            if (employee.Birthdate != null)
            {
                emp.Birthdate = employee.Birthdate;
            }
            if (employee.HireDate != null)
            {
                emp.HireDate = employee.HireDate;
            }
            if (employee.Nationality != null)
            {
                emp.Nationality = employee.Nationality;
            }
            if (employee.PhoneNumber != null)
            {
                emp.PhoneNumber = employee.PhoneNumber;
            }
            if (employee.Position != null)
            {
                emp.Position = employee.Position;
            }
            if (employee.Salary != 0)
            {
                emp.Salary = employee.Salary;
            }
            _context.Employees.Update(emp);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
            
            return Ok();
        }

    }
}

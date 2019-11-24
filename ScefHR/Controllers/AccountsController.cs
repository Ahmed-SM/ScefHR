
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ScefHR.Helpers;
using ScefHR.Models;
using ScefHR.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ScefHR.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {

        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AccountsController(UserManager<AppUser> userManager, DataContext context, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        // POST api/accounts
        [HttpPost("[action]")]
        [Authorize(Policy = "ApiAdmin")]
        public async Task<IActionResult> Post([FromBody]NewEmployee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var checkRole = _context.Roles.FirstOrDefault(r => r.Name == "Employee");
            if (checkRole == null)
            {
                await AddRole("Employee");
            }
            var entity = _context.Entities.Where(e => e.Name == employee.branch).FirstOrDefault();

            if (entity == null)
            {
                return BadRequest("لا يوجد فرع");
            }

            AppUser appUser = new AppUser { Email = employee.email, FirstName=employee.firstname, LastName=employee.lastname, UserName=employee.email};
            
            var result = await _userManager.CreateAsync(appUser, "000000");

            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(appUser, "Employee");

            if (!roleResult.Succeeded) return BadRequest(roleResult.Errors);

            await _context.Employees.AddAsync(new Employee { IdentityId = appUser.Id, Entity = entity});
            entity.NumberOfEmployees = _context.Employees.Where(e => e.Entity == entity).Count();
            _context.Entities.Update(entity);
            await _context.SaveChangesAsync();

            return new OkObjectResult("Account created, Password: 000000");
        }
        [HttpPost("[action]")]
        [Authorize(Policy = "ApiSuper")]
        public async Task<IActionResult> SPost([FromBody]NewEmployee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var checkRole = _context.Roles.FirstOrDefault(r => r.Name == "Employee");
            if (checkRole == null)
            {
                await AddRole("Employee");
            }
            var entity = _context.Entities.Where(e => e.Name == employee.branch).FirstOrDefault();

            if (entity == null)
            {
                return BadRequest("لا يوجد فرع");
            }

            AppUser appUser = new AppUser { Email = employee.email, FirstName = employee.firstname, LastName = employee.lastname, UserName = employee.email };

            var result = await _userManager.CreateAsync(appUser, "000000");

            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(appUser, "Employee");

            if (!roleResult.Succeeded) return BadRequest(roleResult.Errors);

            await _context.Employees.AddAsync(new Employee { IdentityId = appUser.Id, Entity = entity });
            entity.NumberOfEmployees = _context.Employees.Where(e => e.Entity == entity).Count();
            _context.Entities.Update(entity);
            await _context.SaveChangesAsync();

            return new OkObjectResult("Account created, Password: 000000");
        }

        [HttpPost("[action]")]
        [Authorize(Policy = "ApiAdmin")]
        public async Task<IActionResult> PostAdmin([FromBody]NewEmployee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var checkRole = _context.Roles.FirstOrDefault(r => r.Name == "Admin");
            if (checkRole == null)
            {
                await AddRole("Admin");
            }
            var entity = _context.Entities.Where(e => e.Name == employee.branch).FirstOrDefault();

            if (entity == null)
            {
                return BadRequest("لا يوجد فرع");
            }
            AppUser model = new AppUser { Email = employee.email, FirstName = employee.firstname, LastName = employee.lastname, UserName = employee.email };

            var result = await _userManager.CreateAsync(model, "000000");

            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(model, "Admin");

            if (!roleResult.Succeeded) return BadRequest(roleResult.Errors);

            await _context.Employees.AddAsync(new Employee { IdentityId = model.Id, Entity = entity });
            entity.NumberOfEmployees = _context.Employees.Where(e => e.Entity == entity).Count();
            _context.Entities.Update(entity);
            await _context.SaveChangesAsync();

            return new OkObjectResult("Account created");
        }
        [HttpPost("[action]")]
        [Authorize(Policy = "ApiSuper")]
        public async Task<IActionResult> SPostAdmin([FromBody]NewEmployee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var checkRole = _context.Roles.FirstOrDefault(r => r.Name == "Admin");
            if (checkRole == null)
            {
                await AddRole("Admin");
            }
            var entity = _context.Entities.Where(e => e.Name == employee.branch).FirstOrDefault();

            if (entity == null)
            {
                return BadRequest("لا يوجد فرع");
            }
            AppUser model = new AppUser { Email = employee.email, FirstName = employee.firstname, LastName = employee.lastname, UserName = employee.email };

            var result = await _userManager.CreateAsync(model, "000000");

            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(model, "Admin");

            if (!roleResult.Succeeded) return BadRequest(roleResult.Errors);

            await _context.Employees.AddAsync(new Employee { IdentityId = model.Id, Entity = entity });
            entity.NumberOfEmployees = _context.Employees.Where(e => e.Entity == entity).Count();
            _context.Entities.Update(entity);
            await _context.SaveChangesAsync();

            return new OkObjectResult("Account created");
        }

        [HttpGet]
        [Authorize(Policy = "ApiAdmin")]
        private async Task<IActionResult> AddRole(string role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var AppRole = new IdentityRole { Name = role };
            var result = await _roleManager.CreateAsync(AppRole);


            if (!result.Succeeded) return BadRequest(result.Errors);
            await _context.SaveChangesAsync();

            return new OkObjectResult("Role created");
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> initAdmin()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var checkRole = _context.Roles.FirstOrDefault(r => r.Name == "Super");
            if (checkRole == null)
            {
                await AddRole("Super");
            }
            AppUser model = new AppUser { UserName = "SAdmin", FirstName= "SAdmin", LastName= "SAdmin", Email= "SAdmin" };
            var result = await _userManager.CreateAsync(model, "000000");

            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(model, "Super");

            if (!roleResult.Succeeded) return BadRequest(roleResult.Errors);


            await _context.SaveChangesAsync();

            return new OkObjectResult("Admin created");
        }

    }
}


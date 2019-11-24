using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ScefHR.Helpers;
using ScefHR.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ScefHR.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly DataContext _context;


        public AuthController(UserManager<AppUser> userManager, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions, DataContext context)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            _context = context;
            _jwtOptions = jwtOptions.Value;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody]Login credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = await GetClaimsIdentity(credentials.Username, credentials.Password);
            if (identity == null)
            {
                return BadRequest("login_failure" + "Invalid username or password.");
            }
            var user = await _userManager.FindByEmailAsync(credentials.Username);
            var roles = await _userManager.GetRolesAsync(user);
            var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, credentials.Username, _jwtOptions, roles, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> PostAdmin([FromBody]Login credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = await GetAdminClaimsIdentity(credentials.Username, credentials.Password);
            if (identity == null)
            {
                return Unauthorized();
            }
            var user = await _userManager.FindByEmailAsync(credentials.Username);
            var roles = await _userManager.GetRolesAsync(user);

            var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, credentials.Username, _jwtOptions, roles, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty
            var userToVerify = await _userManager.FindByNameAsync(userName);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            var user = _context.UserRoles.Where(i => i.UserId == userToVerify.Id).FirstOrDefault();
            if (user == null)
            {
                return await Task.FromResult<ClaimsIdentity>(null);
            }
            var role = _context.Roles.Where(r => r.Id == user.RoleId).FirstOrDefault();
            if (role == null)
            {
                return await Task.FromResult<ClaimsIdentity>(null);
            }

            // check the credentials
            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id, role.Name));
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }
        private async Task<ClaimsIdentity> GetAdminClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty
            var userToVerify = await _userManager.FindByNameAsync(userName);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            var user = _context.UserRoles.Where(i => i.UserId == userToVerify.Id).FirstOrDefault();
            if (user == null)
            {
                return await Task.FromResult<ClaimsIdentity>(null);
            }
            var role = _context.Roles.Where(r => r.Id == user.RoleId).FirstOrDefault();
            if (role == null || role.Name == "Employee")
            {
                return await Task.FromResult<ClaimsIdentity>(null);
            }

            // check the credentials
            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id, role.Name));
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}

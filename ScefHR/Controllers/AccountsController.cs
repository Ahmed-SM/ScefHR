
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
    public class AccountsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AccountService _accountService;
        private IUserRepository _userRepository;

        public AccountsController(UserManager<AppUser> userManager,
                                  SignInManager<AppUser> signInManager,
                                   IUserRepository userRepository,
                                   AccountService accountService
                                   )
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _accountService = accountService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]AppUser employee)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(employee.Email);
                if ((await _signInManager.PasswordSignInAsync(user.UserName, "HelloWorld_2019", isPersistent: false, lockoutOnFailure: false)).Succeeded)
                {
                    _accountService.Login(user);
                    return Ok(_accountService.Login(user));
                }
            }
            return Ok("error");
        }
        [HttpPost]
        public async Task<IActionResult> Logout() {
            await _signInManager.SignOutAsync();
            return Ok("Singed out");
        }
        [HttpPost]
        public IActionResult Register([FromBody]AppUser employee)
        {
            if (ModelState.IsValid)
            {
                return Ok(_userRepository.Create(employee));
            }
            return BadRequest(ModelState);
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ScefHR.Helpers;
using ScefHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ScefHR.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private DataContext _context;
        public UserRepository(UserManager<AppUser> userManager,
                                  SignInManager<AppUser> signInManager,
                                   DataContext context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<string> Create(AppUser employee)
        {
            IEnumerable<IdentityError> errors = null;
                var user = new AppUser { UserName = employee.Email, Email = employee.Email };
                var result = await _userManager.CreateAsync(user, "HelloWorld_2019");

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    await _context.Employees.AddAsync(new Employee { IdentityId = user.Id });
                    await _context.SaveChangesAsync();
                    return "Signed in";
                }
                errors = result.Errors;
            return errors.ToString();
        }
    }
}

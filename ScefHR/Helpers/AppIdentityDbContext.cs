using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ScefHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScefHR.Helpers
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
    }
}

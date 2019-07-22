using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScefHR.Models
{
    public class Employee 
    {
        public int Id { get; set; }
        public string IdentityId { get; set; }
        public virtual AppUser Identity { get; set; }
        public string Gender { get; set; }
        public virtual Entity Entity { get; set; }
        public string Position { get; set; }
        public DateTime HireDate { get; set; }
        public ServiceForm RequestedForms { get; set; }
    }
}

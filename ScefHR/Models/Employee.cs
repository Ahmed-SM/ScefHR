using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScefHR.Models
{
    public class Employee 
    {
        public int Id { get; set; }
        public string IdentityId { get; set; }
        public virtual AppUser Identity { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public string Address { get; set; }
        public string Birthdate { get; set; }
        public virtual Entity Entity { get; set; }
        public string Position { get; set; }
        public int Salary { get; set; }
        public string HireDate { get; set; }
        public ICollection<ServiceForm> ServiceForms { get; set; }
        public Employee()
        {
            ServiceForms = new Collection<ServiceForm>();
        }
    }
}

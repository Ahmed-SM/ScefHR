using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScefHR.Models
{
    public class IEmployee
    {
         public int Id { get; set; }
         public string PhoneNumber { get; set; }
         public string Firstname { get; set; }
         public string Lastname { get; set; }
         public string Nationality { get; set; }
         public string Email { get; set; }
         public string Address { get; set; }
         public string Birthdate { get; set; }
         public string Position { get; set; }
         public int Salary { get; set; }
         public string HireDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ScefHR.Models
{
    public class ServiceForm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        ICollection<FormField> FormFields { get; set; }

      
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ScefHR.Models
{
    public class ServiceForm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Employee Employee { get; set; }
        public ICollection<FormField> FormFields { get; set; }
        public DateTime IssueDate { get; set; }
        public int Status { get; set; }
        public ServiceForm()
        {
            FormFields = new Collection<FormField>();
        }


    }
}

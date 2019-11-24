using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ScefHR.Models
{
    public class Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfEmployees { get; set; }
        public ICollection<Employee> Employees { get; set; }

        public Entity()
        {
            Employees = new Collection<Employee>();
        }
    }
}
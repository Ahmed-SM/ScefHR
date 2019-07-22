using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ScefHR.Models
{
    public class Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfEmployees { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public ICollection<ServiceForm> ServiceForms { get; set; }

        public Entity()
        {
            Employees = new Collection<Employee>();
            ServiceForms = new Collection<ServiceForm>();
        }
    }
}
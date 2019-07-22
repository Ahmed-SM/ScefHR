using ScefHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScefHR.Services
{
    public interface IEmployeeService
    {
        void Create(Employee newEmployee);
        IQueryable<Employee> Read(int? id);
        void Update();
        void Delete(int id);
    }
}

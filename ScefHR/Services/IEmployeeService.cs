using ScefHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScefHR.Services
{
    public interface IEmployeeService
    {
        Task Create(Employee newEmployee);
        IQueryable Read(string userId);
        IQueryable Read();
        IQueryable Read(int id);
        Task Update(int id, UpdateEmployee value);
        Task Delete(int id);
    }
}

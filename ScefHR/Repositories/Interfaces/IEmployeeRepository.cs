using ScefHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ScefHR.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task Create(Employee employee);
        IQueryable<Employee> Read();
        Task<Employee> Read(int id);
        IQueryable<Employee> ReadByCondition(Expression<Func<Employee, bool>> expression);
        Task Update(Employee employee);
        Task Delete(int id);
    }
}

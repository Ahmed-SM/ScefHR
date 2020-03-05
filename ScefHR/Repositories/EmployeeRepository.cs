using Microsoft.EntityFrameworkCore;
using ScefHR.Helpers;
using ScefHR.Models;
using ScefHR.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ScefHR.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        protected DataContext _context { get; set; }
        public EmployeeRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Create(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }
        public IQueryable<Employee> Read()
        {
            return _context.Set<Employee>();
        }

        public IQueryable<Employee> ReadByCondition(Expression<Func<Employee, bool>> expression)
        {
            return _context.Set<Employee>().Where(expression);
        }
        public async Task<Employee> Read(int id)
        {
            return await _context.Employees.FindAsync(id);
        }
        public async Task Update(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
             _context.Employees.Remove(_context.Employees.Find(id));
            await _context.SaveChangesAsync();
        }
    }
}

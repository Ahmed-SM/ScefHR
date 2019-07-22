using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScefHR.Helpers;
using ScefHR.Models;

namespace ScefHR.Services
{
    public class EmployeeService : IEmployeeService
    {
        private DataContext _context;

        public EmployeeService(DataContext context)
        {
            _context = context;
           // _context.Database.EnsureDeleted();
            //_context.Database.EnsureCreated();
            _context.SaveChanges();
        }
        public void Create(Employee newEmployee)
        {
            _context.Employees.Add(newEmployee);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Employees.Remove(_context.Employees.Find(id));
            _context.SaveChanges();
        }

        public IQueryable<Employee> Read(int? id = null)
        {
            return _context.Set<Employee>();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
        public async Task Create(Employee newEmployee)
        {
            _context.Employees.Add(newEmployee);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            _context.Employees.Remove(_context.Employees.Find(id));
            await _context.SaveChangesAsync();
        }

        public IQueryable Read(string userId)
        {
            var admin = _context.Employees.Where(t => t.IdentityId == userId).Include(e => e.Entity).FirstOrDefault();
            if (admin.Entity != null)
            {
                return _context.Set<Employee>().Where(e => e.Entity == admin.Entity && admin.Id != e.Id).Select(e => new{
                e.Id,
                e.Identity.FirstName,
                e.Identity.LastName,
                e.Entity.Name,
                e.Nationality,
                e.Position,
                e.Salary,
                e.PhoneNumber,
                e.Gender,
                e.Birthdate,
                e.HireDate,
                e.Address
            });
            }
            return null;
        }
        public IQueryable Read()
        {
                return _context.Set<Employee>().Select(e => new {
                    e.Id,
                    e.Identity.FirstName,
                    e.Identity.LastName,
                    e.Entity.Name,
                    e.Nationality,
                    e.Position,
                    e.Salary,
                    e.PhoneNumber,
                    e.Gender,
                    e.Birthdate,
                    e.HireDate,
                    e.Address,
                });
            
        }
        public IQueryable Read(int id)
        {
            return _context.Employees.Where(e => e.Id == id).Select(e => new {
                e.Id,
                e.Identity.FirstName,
                e.Identity.LastName,
                e.Entity.Name,
                e.Nationality,
                e.Position,
                e.Salary,
                e.PhoneNumber,
                e.Gender,
                e.Birthdate,
                e.HireDate,
                e.Address
            });
        }

        public async Task Update(int id, UpdateEmployee value)
        {
            var result = _context.Employees.Find(id);
            if (result != null)
            {
                var user = _context.Users.Find(result.IdentityId);

                user.FirstName = value.Firstname;
                user.LastName = value.Lastname;
                result.Identity = user;
                result.Entity = _context.Entities.Where(n => n.Name == value.Branch).FirstOrDefault();
                result.Position = value.Position;
                result.Salary = value.Salary;
                result.Nationality = value.Nationality;
                result.Gender = value.Gender;
                result.Address = value.Address;
                result.Birthdate = value.Birthdate;
                result.PhoneNumber = value.PhoneNumber;
                result.HireDate = value.HireDate;

                _context.Employees.Update(result);
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw e;
            }

        }
    }
}

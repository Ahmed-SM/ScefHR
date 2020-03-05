using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ScefHR.Helpers;
using ScefHR.Models;
using ScefHR.Repositories.Interfaces;

namespace ScefHR.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository _employeeRepository;
        private IEntityRepository _entityRepository;
        private IUserRepository _userRepository;

        public EmployeeService(IEmployeeRepository employeeRepository, IEntityRepository entityRepository, IUserRepository UserRepository)
        {
            _employeeRepository = employeeRepository;
            _entityRepository = entityRepository;
            _userRepository = UserRepository;
            // _context.Database.EnsureDeleted();
            //_context.Database.EnsureCreated();
            //_context.SaveChanges();
        }
        public async Task Create(Employee newEmployee)
        {
            await _employeeRepository.Create(newEmployee);

        }

        public async Task Delete(int id)
        {
            await _employeeRepository.Delete(id);
        }

        public IQueryable Read(string userId)
        {
            var admin = _employeeRepository.ReadByCondition(t => t.IdentityId == userId).Include(e => e.Entity).FirstOrDefault();
            if (admin.Entity != null)
            {
                return _employeeRepository.ReadByCondition(e => e.Entity == admin.Entity && admin.Id != e.Id).Select(e => new{
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
                return _employeeRepository.Read().Select(e => new {
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
            return _employeeRepository.ReadByCondition(e => e.Id == id).Select(e => new {
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

            var result = await _employeeRepository.Read(id);
            if (result != null)
            {
                var user = _userRepository.Read(result.IdentityId);

                user.FirstName = value.Firstname;
                user.LastName = value.Lastname;
                result.Identity = user;
                result.Entity = _entityRepository.ReadByCondition(n => n.Name == value.Branch).FirstOrDefault();
                result.Position = value.Position;
                result.Salary = value.Salary;
                result.Nationality = value.Nationality;
                result.Gender = value.Gender;
                result.Address = value.Address;
                result.Birthdate = value.Birthdate;
                result.PhoneNumber = value.PhoneNumber;
                result.HireDate = value.HireDate;

                await _employeeRepository.Update(result);
            }
        }
    }
}

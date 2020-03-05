using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ScefHR.Helpers;
using ScefHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace ScefHR.Services
{
    public class UserRepository : IUserRepository
    { 
        private DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public Task Create(AppUser entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<AppUser> Read()
        {
            throw new NotImplementedException();
        }

        public AppUser Read(string id)
        {
            return _context.Set<AppUser>().Find(id);
        }

        public IQueryable<AppUser> ReadByCondition(Expression<Func<AppUser, bool>> expression)
        {
            return _context.Set<AppUser>().Where(expression);
        }

        public Task Update()
        {
            throw new NotImplementedException();
        }
    }
}

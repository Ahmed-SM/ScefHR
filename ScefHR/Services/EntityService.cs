using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ScefHR.Helpers;
using ScefHR.Models;

namespace ScefHR.Services
{
    
    public class EntityService : IEntityService
    {
        private DataContext _context;

        public EntityService(DataContext context)
        {
            _context = context;
            _context.SaveChanges();
        }

        public async Task Create(string name)
        {
            var exName = _context.Entities.Where(n => n.Name == name).FirstOrDefault();
            if (exName != null) return;
            Entity newEntity = new Entity { Name = name };
            _context.Entities.Add(newEntity);
            await _context.SaveChangesAsync();

        }

        public async Task Delete(int id)
        {
            _context.Entities.Remove(_context.Entities.Find(id));
            await _context.SaveChangesAsync();
        }

        public IQueryable Read(string userId)
        {
            var entityName = _context.Employees.Where(t => t.IdentityId == userId).Select(n =>  n.Entity.Name).First();
            if (entityName != null)
            {
                var res = _context.Entities.Where(e => e.Name == entityName);
                return res;
            }
            return null;
           
        }
        public IQueryable AdminRead()
        {
            
            return _context.Set<Entity>();
        }

        public Task Update(int? id)
        {
            throw new NotImplementedException();
        }

        public IQueryable SRead()
        {
            return _context.Set<Entity>();
        }
    }
}

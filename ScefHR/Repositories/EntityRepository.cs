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
    public class EntityRepository : IEntityRepository
    {
        protected DataContext _context { get; set; }

        public EntityRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Create(Entity entity)
        {
            await _context.Entities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Entity> ReadByCondition(Expression<Func<Entity, bool>> expression)
        {
            return _context.Set<Entity>().Where(expression);
        }

        public IQueryable<Entity> Read()
        {
            return _context.Set<Entity>();
        }
        public Entity Read(int id)
        {
            return _context.Entities.Find(id);
        }

        public Task Update()
        {
            throw new NotImplementedException();
        }
    }
}

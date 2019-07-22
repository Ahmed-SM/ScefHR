using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public string Create(Entity newEntity)
        {
            if (string.IsNullOrEmpty(newEntity.Name))
            {
                return "Some Fields are empty";  
            }
            _context.Entities.Add(newEntity);
            _context.SaveChanges();
            return "New Entity was added";

        }

        public void Delete(int id)
        {
            _context.Entities.Remove(_context.Entities.Find(id));
            _context.SaveChanges();
        }

        public IQueryable<Entity> Read(int? id)
        {
            return _context.Set<Entity>();
        }

        public void Update(int? id)
        {
            throw new NotImplementedException();
        }
    }
}

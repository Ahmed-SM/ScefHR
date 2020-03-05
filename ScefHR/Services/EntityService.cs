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
    
    public class EntityService : IEntityService
    {
        private IEntityRepository _entityRepository;
        private IEmployeeRepository _employeeRepository;

        public EntityService(DataContext context, IEntityRepository entityRepository, IEmployeeRepository employeeRepository)
        {
            _entityRepository = entityRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task Create(string name)
        {
            Entity entity = new Entity { Name = name };
            await _entityRepository.Create(entity);
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable Read(string userId)
        {
            var entityName = _employeeRepository.ReadByCondition(t => t.IdentityId == userId).Select(n =>  n.Entity.Name).First();
            if (entityName != null)
            {
                var res = _entityRepository.ReadByCondition(e => e.Name == entityName);
                return res;
            }
            return null;
           
        }
        public IQueryable AdminRead()
        {
            return _entityRepository.Read();
        }
        public IQueryable SRead()
        {
            return _entityRepository.Read();
        }
        public Task Update(int? id)
        {
            throw new NotImplementedException();
        }
    }
}

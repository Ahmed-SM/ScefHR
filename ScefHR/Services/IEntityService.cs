using ScefHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScefHR.Services
{
    public interface IEntityService
    {
        string Create(Entity newEntity);
        IQueryable<Entity> Read(int? id);
        void Update(int? id);
        void Delete(int id);
    }
}

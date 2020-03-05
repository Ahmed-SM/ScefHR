using ScefHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ScefHR.Repositories.Interfaces
{
    public interface IEntityRepository
    {
        Task Create(Entity entity);
        IQueryable<Entity> Read();
        Entity Read(int id);
        IQueryable<Entity> ReadByCondition(Expression<Func<Entity, bool>> expression);
        Task Update();
        Task Delete(int id);
    }
}
